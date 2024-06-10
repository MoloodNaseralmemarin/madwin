using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Settings;
using System.Linq;

public  class Tools
{
    private  readonly Shop2CityContext _context;
    public Tools(Shop2CityContext context)
    {
        _context = context;
    }
    public Setting GetTools()
    {
        var q = _context.Setting.FirstOrDefault();
        return q;
    }
}

