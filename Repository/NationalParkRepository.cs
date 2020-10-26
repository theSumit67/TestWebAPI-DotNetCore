
using System.Collections.Generic;
using TestWebAPI.Models;
using TestWebAPI.Data;
using System.Linq;
using TestWebAPI.Repository.IRepository;

public class NationalParkRepository : INationalParkRepository
{
    private readonly ApplicationDbContext _db;

    public NationalParkRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool CreateNationalPark(NationalPark nationalPark)
    {
        _db.NationalParks.Add(nationalPark);
        return Save();
    }

    public bool DeleteNationalPark(NationalPark nationalPark)
    {
        _db.NationalParks.Remove(nationalPark);
        return Save();
    }

    public NationalPark GetNationalPark(int nationalParkId)
    {
        return _db.NationalParks.FirstOrDefault(item => item.Id == nationalParkId); // firstOrDefault Linq
    }

    public ICollection<NationalPark> GetNationalParks()
    {
        return _db.NationalParks.OrderBy(item => item.Name).ToList();
    }

    public bool NationalParkExists(string name)
    {
        return _db.NationalParks
        .Any(item => item.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public bool NationalParkExists(int id)
    {
        return _db.NationalParks.Any(item => item.Id == id);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public bool UpdateNationalPark(NationalPark nationalPark)
    {
        _db.NationalParks.Update(nationalPark);
        return Save(); 
    }
}