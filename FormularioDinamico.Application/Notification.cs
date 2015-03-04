using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public class Notification
    {
        private ICollection<string> _errors = new List<string>();

        public ICollection<string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public bool HasErrors
        {
            get { return 0 != Errors.Count; }
        }
    }

}
