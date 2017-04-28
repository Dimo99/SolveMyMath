using SolveMath.Data;

namespace SolveMath.Services
{
    public abstract class Service
    {
        protected Service()
        {
            Context = new SolveMathContext();
        }

        protected SolveMathContext Context { get; set; }
    }
}