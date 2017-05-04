using SolveMath.Data;
using SolveMath.Data.Interfaces;

namespace SolveMath.Services
{
    public abstract class Service
    {
        protected Service()
        {
            Context = new SolveMathContext();
        }

        protected Service(ISolveMathContext context)
        {
            Context = context;
        }
        protected ISolveMathContext Context { get; set; }
    }
}