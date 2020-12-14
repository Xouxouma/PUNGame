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

    /**
     * Return the index of the best elem : 1 or 2. If they are neutral: returns 0. Returns -1 if it's one Elem is unknown (shouldn't occur).
     * */
    public static int BestElem(Elems elem1, Elems elem2)
    {
        switch(elem1)
        {
            case Elems.Air:
                switch (elem1)
                {
                    case Elems.Air:
                        return 0;
                    case Elems.Water:
                        return 1;
                    case Elems.Fire:
                        return 2;
                    default:
                        return -1;
                }
            case Elems.Water:
                switch (elem1)
                {
                    case Elems.Air:
                        return 2;
                    case Elems.Water:
                        return 0;
                    case Elems.Fire:
                        return 1;
                    default:
                        return -1;
                }
            case Elems.Fire:
                switch (elem1)
                {
                    case Elems.Air:
                        return 1;
                    case Elems.Water:
                        return 2;
                    case Elems.Fire:
                        return 0;
                    default:
                        return -1;
                }
            default:
                return -1;
        }
    }
    
}

