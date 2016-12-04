using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DBPlayground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MongoDemo.InsertData();
            MongoDemo.GetData();
        }
    }
}
