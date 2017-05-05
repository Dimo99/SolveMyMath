using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class ProblemBindingModel
    {
        [AllowHtml]
        public string Problem { get; set; }
    }
}
