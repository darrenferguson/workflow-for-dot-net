using System.Collections.Generic;
using System.Reflection;
using log4net;
using Moriyama.Workflow.Umbraco6.Application.Interfaces;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Application
{
    public class UmbracoWorkflowInstantiationCriteriaValidationService : IUmbracoWorkflowInstantiationCriteriaValidationService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly UmbracoWorkflowInstantiationCriteriaValidationService Service = new UmbracoWorkflowInstantiationCriteriaValidationService();

        public static UmbracoWorkflowInstantiationCriteriaValidationService Instance
        {
            get { return Service; }
        }

        private UmbracoWorkflowInstantiationCriteriaValidationService() { }

        public bool IsCriteriaValid(UmbracoWorkflowInstantiationCriteria criteria, User u)
        {
            if(criteria.Users.Count > 0) Log.Debug(string.Format("Validating criteria for users {0}. Current user {1}", string.Join(", ", criteria.Users), u.Id));
            Log.Debug(string.Format("Users contains user {0}", criteria.Users.Contains(u.Id)));

            if(criteria.UserTypes.Count > 0) Log.Debug(string.Format("User type criteria {0} current user {1}", string.Join(", ", criteria.UserTypes), u.UserType.Id));
            Log.Debug(string.Format("UserTypes contains user {0}", criteria.UserTypes.Contains(u.UserType.Id)));

            var userMatches = UserMatches(criteria.Users, u);
            var userTypeMatches = UserTypeMatches(criteria.UserTypes, u);

            
                Log.Debug(string.Format("return: {0} and {1}", userMatches, userTypeMatches));
                if((criteria.Users.Count > 0) && (criteria.UserTypes.Count > 0))
                {
                    if (criteria.CriteriaOperand == "and")
                    {
                        return userMatches && userTypeMatches;
                    }
                    return userMatches || userTypeMatches;
                }

                if(criteria.Users.Count > 0)
                {
                    return userMatches;
                }

                if(criteria.UserTypes.Count > 0)
                {
                    return userTypeMatches;
                }
            return true;
        }

        protected bool UserMatches(IList<int> criteriaUsers, User theUser)
        {
            return criteriaUsers.Contains(theUser.Id);
        }

        protected bool UserTypeMatches(IList<int> criteriaUserTypes, User theUser)
        {
            return criteriaUserTypes.Contains(theUser.UserType.Id);
        }
    }
}
