using System;
using umbraco.cms.businesslogic;

namespace FergusonMoriyam.Workflow.Umbraco.Domain
{
    public static class Extension
    {
        public static bool IsDocument(this CMSNode node)
        {
            return node.nodeObjectType == new Guid("C66BA18E-EAF3-4CFF-8A22-41B16D66A972");
        }

        public static bool IsMedia(this CMSNode node)
        {
            return node.nodeObjectType == new Guid("B796F64C-1F99-4FFB-B886-4BF4BC011A9C");
        }
    }
}
