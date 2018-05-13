using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5
{
    public static class LinkTarget
    {
        public const string NewTabOrWindow = "_blank";
        public const string SameFrameAsClicked = "_self";
        public const string ParentFrame = "_parent";
        public const string FullWindowBody = "_top";
    }
}
