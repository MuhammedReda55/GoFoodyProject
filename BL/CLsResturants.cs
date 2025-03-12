using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public interface IResturant
    {
        public List<Restaurant> GetAll(); // تم إزالة الـ id
        public Restaurant GetById(int id);
        public bool Save(Restaurant exam); // تم تغيير الاسم
        public bool Delete(int id);
        public List<Restaurant> GetByName(string examType);
        public Restaurant GetByIdFromView(int id);
        public bool DeleteFromView(int id);
        
    }

    public class CLsResturants : IResturant
    {
        ConfigContext _context;

        public CLsResturants(ConfigContext context)
        {
            _context = context;
        }

        public List<Restaurant> GetByName(string name)
        {
            try
            {
                var filteredItems = _context.TbRestaurants.Where(item => item.RestaurantName == name).ToList();
                return filteredItems;
            }
            catch
            {
                return new List<Restaurant>();
            }
        }

        public List<Restaurant> GetAll() // تم تعديل الـ Method
        {
            try
            {
                var lstItems = _context.TbRestaurants.ToList();
                return lstItems;
            }
            catch
            {
                return new List<Restaurant>();
            }
        }

        public Restaurant GetById(int id)
        {
            try
            {
                return _context.TbRestaurants.FirstOrDefault(a => a.RestaurantId == id);
            }
            catch
            {
                return null;
            }
        }

        public bool Save(Restaurant exam) // تم تعديل الـ Method
        {
            try
            {
                if (exam.RestaurantId == 0)
                {
                    _context.TbRestaurants.Add(exam);
                }
                else
                {
                    _context.Entry(exam).State = EntityState.Modified;
                }

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var exam = GetById(id);
                if (exam != null)
                {
                    _context.TbRestaurants.Remove(exam);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }






        public Restaurant GetByIdFromView(int id)
        {
            try
            {
                return _context.TbRestaurants.FirstOrDefault(a => a.RestaurantId == id);
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteFromView(int id)
        {
            try
            {
                var exam = GetByIdFromView(id);
                if (exam != null)
                {
                    _context.TbRestaurants.Remove(exam);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

      
    }
}