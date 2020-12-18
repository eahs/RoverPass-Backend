﻿using ADSBackend.Data;
using ADSBackend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ADSBackend.Configuration
{
    public class ApplicationDbSeed
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbSeed(ApplicationDbContext context)
        {
            _context = context;

        }

        public string GetJson(string seedFile)
        {
            var file = System.IO.File.ReadAllText(Path.Combine("Configuration", "SeedData", seedFile));

            return file;
        }

        public void SeedDatabase<TEntity>(string jsonFile, DbSet<TEntity> dbset, bool preserveOrder = false) where TEntity : class
        {
            var records = JsonConvert.DeserializeObject<List<TEntity>>(GetJson(jsonFile));

            if (records?.Count > 0)
            {
                if (!preserveOrder)
                {
                    _context.AddRange(records);
                    _context.SaveChanges();
                }
                else
                {
                    foreach (var record in records)
                    {
                        dbset.Add(record);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void SeedDatabase()
        {
            CreatePassTypes();
        }

        private void CreatePassTypes()
        {
            var types = _context.PassType.OrderBy(pt => pt.Name).ToList();
            if (types == null || types.Count == 0)
            {
                SeedDatabase<PassType>("PassTypes.json", _context.PassType, true);
            }
            else
            {
                var ptypes = JsonConvert.DeserializeObject<List<PassType>>(GetJson("PassTypes.json"));
                foreach (var type in ptypes)
                {
                    var exists = types.FirstOrDefault(p => p.Name == type.Name);

                    if (exists == null)
                    {
                        _context.PassType.Add(type);
                        _context.SaveChanges();
                    }
                }

            }

        }

    }
}