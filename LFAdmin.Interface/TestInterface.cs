using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAdmin.Interface
{
    public interface  TestInterface
    {
        TestService ts = new TestService();

        void work(string s);

    }
}
