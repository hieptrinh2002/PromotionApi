using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromotionApi.Models;
using PromotionApi.Models.Dtos;

namespace PromotionApi.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly PromotionDbContext _context;
        private readonly IMapper _mapper;

        public PromotionController(PromotionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/promotions
        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            try
            {
                var promotions = await _context.Promotions.ToListAsync();
                return Ok(new { status = "SUCCESS", message = "Promotions retrieved successfully.", data = promotions });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "ERROR", message = ex.Message });
            }
        }

        // GET: api/promotions/merchant/{merchantId}
        [HttpGet("merchant/{merchantId}")]
        public async Task<IActionResult> GetAllPromotionsByMerchantId(string merchantId)
        {
            try
            {
                var promotions = await _context.Promotions.Where(m => m.MerchantId == merchantId).ToListAsync();
                return Ok(new { status = "SUCCESS", message = "Promotions retrieved successfully.", data = promotions });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "ERROR", message = ex.Message });
            }
        }


        // GET: api/promotions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionById(string id)
        {
            try
            {
                var promotion = await _context.Promotions.FindAsync(id);

                if (promotion == null)
                {
                    return NotFound(new { status = "ERROR", message = "Promotion not found." });
                }

                return Ok(new { status = "SUCCESS", message = "Promotion retrieved successfully.", data = promotion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "ERROR", message = ex.Message });
            }
        }

        // POST: api/promotions
        [HttpPost]
        public async Task<IActionResult> CreatePromotion(CreatePromotionRequestDto promotionDto)
        {
            try
            {
                Promotion newPromotion = _mapper.Map<Promotion>(promotionDto);
                _context.Promotions.Add(newPromotion);
                await _context.SaveChangesAsync();

                return Ok(new { status = "SUCCESS", message = "Promotion created successfully.", data = newPromotion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "ERROR", message = ex.Message });
            }
        }

        // DELETE: api/promotions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(string id)
        {
            try
            {
                var promotion = await _context.Promotions.FindAsync(id);
                if (promotion == null)
                {
                    return NotFound(new { status = "ERROR", message = "Promotion not found." });
                }

                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();

                return Ok(new { status = "SUCCESS", message = "Promotion deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "ERROR", message = ex.Message });
            }
        }

        [HttpPost("valid-order")]
        public async Task<IActionResult> CheckValidPromotionOfOrder(OrderDto orderDto)
        {
            var promotion = await _context.Promotions.Where(p => p.Code == orderDto.PromotionCode).FirstOrDefaultAsync();

            if (promotion == null)
            {
                return Ok(new { status = "FAILED", message = "Promotion code does not exist !", data = orderDto });
            }

            if (orderDto.Amount < promotion.Condition)
            {
                return Ok(new { status = "FAILED", message = $"Order amount must be greater than {promotion.Condition} !", data = orderDto });
            }

            if (!promotion.IsQuantityAvailable())
            {
                return Ok(new { status = "FAILED", message = $"Promotion is not available due to quantity limit !", data = orderDto });
            }

            if (promotion.IsExpired())
            {
                return Ok(new { status = "FAILED", message = $"Promotion is expired !", data = orderDto });
            }

            if (promotion.isNotStart())
            {
                return Ok(new { status = "FAILED", message = $"Promotion has not yet started !", data = orderDto });
            }

            return Ok(new { status = "SUCCESS", message = $"Apply promotion successfully !", data = orderDto });
        }


        [HttpGet("valid-for-amount/{amount}")]
        public async Task<IActionResult> GetListAvailablePromotionsForOrder(long amount)
        {
            // Lấy tất cả các promotion thỏa mãn các điều kiện
            var eligiblePromotions = await _context.Promotions
                .Where(p => p.QuantityAvailable > 0 && p.DateExpire > DateTime.Now && p.DateStart < DateTime.Now && amount >= p.Condition)
                .ToListAsync();

            if (eligiblePromotions == null || eligiblePromotions.Count == 0)
            {
                return Ok(
                     new
                     {
                         status = "NOT_FOUND",
                         message = $"There are currently no promotions valid for this order !",
                         data = new List<Promotion>()
                     }
                );
            }
            else
            {
                return Ok(
                     new
                     {
                         status = "SUCCESS",
                         message = $"Get successful promotional packages !",
                         data = eligiblePromotions
                     }
                );
            }
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyPromotionForOrder(OrderDto orderDto)
        {
            // Step 1: Kiểm tra PromotionCode có tồn tại không
            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(p => p.Code == orderDto.PromotionCode);

            if (promotion == null)
            {
                return Ok(new { status = "FAILED", message = "Promotion code does not exist !", data = orderDto });
            }

            if (orderDto.Amount < promotion.Condition)
            {
                return Ok(new { status = "FAILED", message = $"Order amount must be greater than {promotion.Condition} !", data = orderDto });
            }

            // Kiểm tra xem Promotion có IsQuantityAvailable = true và IsExpired = false
            if (!promotion.IsQuantityAvailable())
            {
                return Ok(new { status = "FAILED", message =  $"Promotion is not available due to quantity limit !", data = orderDto });
            }

            if (promotion.IsExpired())
            {
                return Ok(new { status = "FAILED", message = $"Promotion is expired. !", data = orderDto });
            }
            if (promotion.isNotStart())
            {
                return Ok(new { status = "FAILED", message = $"Promotion has not yet started !", data = orderDto });
            }

            promotion.QuantityAvailable -= 1;
            _context.Promotions.Update(promotion);
            await _context.SaveChangesAsync();
            
            return Ok(new { status = "SUCCESS", message = $"Apply promotion successfully !", data = orderDto });
        }

    }
}
