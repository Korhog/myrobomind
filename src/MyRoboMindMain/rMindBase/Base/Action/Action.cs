using System.Linq;
using System.Collections.Generic;

namespace rMind.Action
{
    public interface IAction
    {
        /// <summary> Execute action </summary>
        void Up();
        /// <summary> Revert action </summary>
        void Down();
    }

    public class BaseAction : IAction
    {
        public void Up()
        {

        }

        public void Down()
        {

        }
    }

    public class BaseActionList : List<BaseAction>
    {
        public void Revert()
        {
            if (Count == 0)
                return;

            var last = this.LastOrDefault();
            last.Down();
            Remove(last);
        }
    }
}
