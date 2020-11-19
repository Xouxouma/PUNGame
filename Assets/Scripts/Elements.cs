using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum Elems
{
    Air,
    Water,
    Fire
}

class ElemsHandler
{
    public static string ElemIndexToString(int _elemIndex)
    {
        Elems _elem = (Elems)_elemIndex;
        return _elem.ToString();
    }
    
}

