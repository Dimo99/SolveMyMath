using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;

namespace SolveMath.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ProblemBindingModel problemBindingModel)
        {
            string problem = problemBindingModel.Problem;
            Regex regex = new Regex(@"\d*x\^2[+-]\d*x[+-]\d+=0");
            if (!regex.IsMatch(problem))
            {
                throw new InvalidOperationException("Съжеляваме, но ме можем да решим вашия проблем");
            }
            int A, B, C;
            string a = problem.Substring(0, problem.IndexOf("x^2"));
            if (string.IsNullOrEmpty(a))
            {
                A = 1;
            }
            else
            {
                A = int.Parse(a);
            }
            string b = problem.Substring(problem.IndexOf("x^2") + 3,
                problem.Substring(problem.IndexOf("x^2") + 2).IndexOf("x")-1);
            if (string.IsNullOrEmpty(b))
            {
                B = 1;
            }
            else
            {
                B = int.Parse(b);
            }
            string c = problem.Substring(problem.LastIndexOf("x")+1, problem.IndexOf("=")- problem.LastIndexOf("x")-1);
            if (string.IsNullOrEmpty(c))
            {
                C = 1;
            }
            else
            {
                C = int.Parse(c);
            }
            SolutionViewModel model = new SolutionViewModel() {Steps = new List<string>()};
            model.Steps.Add(problem);
            string firstStep = $"Намираме, a={A},b={B},c={C}";
            model.Steps.Add(firstStep);
            if (A == 0)
            {
                if (B == 0 && C == 0)
                {
                    model.Steps.Add("Всяко x е решение");
                    return this.View("Solution", model.Steps);
                }
                if (B == 0)
                {
                    model.Steps.Add("Няма решение");
                    return this.View("Solution", model.Steps);
                }
                model.Steps.Add($"X = {-C}/{B}");
                return this.View("Solution", model.Steps);
            }
            int D = B * B - 4 * A * C;
            if (D < 0)
            {
                model.Steps.Add($"D={D}<0");
                model.Steps.Add($"Няма решение!");
                return this.View("Solution", model.Steps);
            }
            model.Steps.Add($"D=b^2-4*a*c");
            model.Steps.Add($"D={B}^2-4*{A}*{C}");
            model.Steps.Add($"D={D}");
            if (IsPerfectSquare(D))
            {
                model.Steps.Add($"D={Math.Sqrt(D)}^2");
            }
            model.Steps.Add($"Решенията са:");
            model.Steps.Add($"x1=({-B}+Sqrt{D})/2*{A}={(-B + Math.Sqrt(D)) / 2 * A}");
            model.Steps.Add($"x2=({-B}-Sqrt{D})/2*{A}={(-B - Math.Sqrt(D)) / 2 * A}");
            return this.View("Solution", model.Steps);
        }
        //public ActionResult Solution(List<string> steps)
        //{
        //    return this.View(steps);
        //}
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        #region Helpers
        bool IsPerfectSquare(double input)
        {
            var sqrt = Math.Sqrt(input);
            return Math.Abs(Math.Ceiling(sqrt) - Math.Floor(sqrt)) < Double.Epsilon;
        }
        #endregion
    }
}