using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public enum Characteristics
    {
       WS = 0,
       BS = 1,
       S = 2,
       T = 3,
       I = 4,
       Agi = 5,
       Dex = 6,
       Int = 7,
       WP = 8,
       Fel = 9
    }
    public enum Species
    {
        Human = 0,
        Dwarf= 1,
        Halfling = 2,
        Welf = 3,
        Helf = 4
    }

    public enum AdvanceLevel
    {
        None = 0,           // No Advances in career, no trappings
        Beginner = 1,       // Some Advances in career (characteristics have 0-3/5-8/10-13/15-18, skills have 0-5/5-10/10-15/15-20, no talents, a few trappings
        Experienced = 2,    // 4 * 4/3/2/1 Many advances in career (characteristics have 3-5/8-10/13-15/18-20, skills have 3-5/8-10/13-15/18-20, 0-1 talent, most trappings
        Complete = 3,        // 5 * 4/3/2/1 4  All advances in career (characteristics have 5/10/15/20, skills have 5/10/15/20, all trappings, 1-2 talents
        Beyond = 4            // 
    }
}
