using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using SurveyAppService.DataObjects;
using SurveyAppService.Models;

namespace SurveyAppService.Controllers
{
    public class SurveyQuestionController : TableController<SurveyQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SurveyQuestion>(context, Request);
        }

        // GET tables/SurveyQuestion
        public IQueryable<SurveyQuestion> GetAllSurveyQuestion()
        {
            return Query(); 
        }

        // GET tables/SurveyQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SurveyQuestion> GetSurveyQuestion(string id)
        {
            return Lookup(id);
        }
    }
}
