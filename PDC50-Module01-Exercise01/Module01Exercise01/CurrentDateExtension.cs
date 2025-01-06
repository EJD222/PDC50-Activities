using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module01Exercise01
{
    public class CurrentDateExtension : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return DateTime.Now.ToString("D");
        }
    }
}
