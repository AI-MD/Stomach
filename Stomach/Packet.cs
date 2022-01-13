using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomach
{ 
    class Packet
    {
        string imgPath;
        string stoTag;
        string stoProb;

        public string GetPath()
        {
            return imgPath;
        }
        public void SetPath(string _path)
        {
            imgPath = _path;
        }
        public string GetTag()
        {
            return stoTag;
        }
        public void SetTag(string _tag)
        {
            stoTag = _tag;
        }
        public string GetProb()
        {
            return stoProb;
        }
        public void SetProb(string _prob)
        {
            stoProb = _prob;
        }


    }
}
