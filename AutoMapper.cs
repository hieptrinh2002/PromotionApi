using AutoMapper;
using PromotionApi.Models;
using PromotionApi.Models.Dtos;

namespace PromotionApi
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
        CreateMap<Promotion, CreatePromotionRequestDto>().ReverseMap();

        }
    }
}
