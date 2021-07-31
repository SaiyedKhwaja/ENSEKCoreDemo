using System;

namespace ENSEKCoreWebApp.Helpers
{
    public static class HtmlHelpers
    {
        public static string GetRandomID(string prefix = "RND")
        {
            return string.Format("{0}{1}", prefix, Guid.NewGuid().ToString("N"));
        }
      
      
    }
}
