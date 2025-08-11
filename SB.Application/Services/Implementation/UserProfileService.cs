using Microsoft.EntityFrameworkCore;
using SB.Application.Services.Interface;
using SB.Domain.Entities;
using SB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Implementation
{
    public class UserProfileService: IUserProfileService
    {
        private readonly SupabaseDbContext _context;

        public UserProfileService(SupabaseDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            // Map SB.Application.UserProfile to SB.Domain.Entities.UserProfile
            var domainUser = new SB.Domain.Entities.UserProfile
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Country = user.Country,
                ZipCode = user.ZipCode,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                SSN = user.SSN,
                DrivingLicence = user.DrivingLicence,
                LanguageKnowns = user.LanguageKnowns,
                Skills = user.Skills,
                ExperinceOfYear = user.ExperinceOfYear,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Embedding = null
            };

            _context.UserProfiles.Add(domainUser);
            await _context.SaveChangesAsync();

            // Optionally, update the input user with the generated Id
            user.Id = domainUser.Id;
            
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null) return false;
            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync() =>
            await _context.UserProfiles
                .Select(user => new UserProfile
                {
                    Id = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    Country = user.Country,
                    ZipCode = user.ZipCode,
                    DateOfBirth = user.DateOfBirth,
                    Qualification = user.Qualification,
                    SSN = user.SSN,
                    DrivingLicence = user.DrivingLicence,
                    LanguageKnowns = user.LanguageKnowns,
                    Skills = user.Skills,
                    ExperinceOfYear = user.ExperinceOfYear,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAt = user.CreatedAt,
                  //  Embedding = user.Embedding
                })
                .ToListAsync();

        // Fix for CS0029: Ensure the correct namespace is used for the UserProfile type.
        public async Task<UserProfile?> GetByIdAsync(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);

            // Fix for CS8603: Add nullability handling to ensure the method can return null safely.
            if (user == null)
            {
                return null;
            }

            // Map SB.Domain.Entities.UserProfile to SB.Application.UserProfile if necessary.
            return new UserProfile
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Country = user.Country,
                ZipCode = user.ZipCode,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                SSN = user.SSN,
                DrivingLicence = user.DrivingLicence,
                LanguageKnowns = user.LanguageKnowns,
                Skills = user.Skills,
                ExperinceOfYear = user.ExperinceOfYear,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
               // Embedding = user.Embedding
            };
        }

        public async Task<UserProfile?> GetUserByEmailAsync(string email)
        {
            var user = await _context.UserProfiles.FindAsync(email);

            // Fix for CS8603: Add nullability handling to ensure the method can return null safely.
            if (user == null)
            {
                return null;
            }

            // Map SB.Domain.Entities.UserProfile to SB.Application.UserProfile if necessary.
            return new UserProfile
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Country = user.Country,
                ZipCode = user.ZipCode,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                SSN = user.SSN,
                DrivingLicence = user.DrivingLicence,
                LanguageKnowns = user.LanguageKnowns,
                Skills = user.Skills,
                ExperinceOfYear = user.ExperinceOfYear,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                //Embedding = user.Embedding
            };
        }

        public async Task<bool> UpdateAsync(UserProfile user)
        {
            if (!_context.UserProfiles.Any(e => e.Id == user.Id)) return false;
            var domainUser = new SB.Domain.Entities.UserProfile
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Country = user.Country,
                ZipCode = user.ZipCode,
                DateOfBirth = user.DateOfBirth,
                Qualification = user.Qualification,
                SSN = user.SSN,
                DrivingLicence = user.DrivingLicence,
                LanguageKnowns = user.LanguageKnowns,
                Skills = user.Skills,
                ExperinceOfYear = user.ExperinceOfYear,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
               // Embedding = user.Embedding
            };
            _context.Entry(domainUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
