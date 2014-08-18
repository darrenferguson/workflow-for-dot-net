using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Application.Interfaces
{
    public interface IUmbracoWorkflowInstantiationCriteriaValidationService
    {
        bool IsCriteriaValid(UmbracoWorkflowInstantiationCriteria criteria, User u);

    }
}
