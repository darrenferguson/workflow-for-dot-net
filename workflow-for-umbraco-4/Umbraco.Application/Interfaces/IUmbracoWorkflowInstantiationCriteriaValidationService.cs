using FergusonMoriyam.Workflow.Umbraco.Domain;
using umbraco.BusinessLogic;

namespace FergusonMoriyam.Workflow.Umbraco.Application.Interfaces
{
    public interface IUmbracoWorkflowInstantiationCriteriaValidationService
    {
        bool IsCriteriaValid(UmbracoWorkflowInstantiationCriteria criteria, User u);

    }
}
