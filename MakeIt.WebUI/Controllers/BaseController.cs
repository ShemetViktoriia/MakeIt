using AutoMapper;
using MakeIt.WebUI.Filters;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [ExceptionLogger]
    [VisitLogger]
    public class BaseController : Controller
    {
        protected IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}