using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Test.DB;
using Test.Repository.Repositories;
using Test.Repository.RepositoryInerface;

namespace VehicleTracking.API.Test.TestRepository
{
    public class TestRepositoriesSinglton
    {
		private static TestRepositoriesSinglton _instance;
		private static readonly object padlock = new object();
		public  IUserRepository _userRepository;
		public  IRoleRepository _roleRepository;
		public  IDeviceRepository _deviceRepository;
		public  IDeviceLogRepository _deviceLogRepository;
		
		private TestRepositoriesSinglton()
		{
			CreateDatabase();			
		}

		public static TestRepositoriesSinglton GetInstance()
		{
			if (_instance == null)
				lock (padlock)
				{
					if (_instance == null)
						_instance = new TestRepositoriesSinglton();
				}

			return _instance;
		}

		private void CreateDatabase()
		{
			var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "UnitTest.db");
			if (File.Exists(dataPath)) File.Delete(dataPath);

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite("Filename=./UnitTest.db")
                .Options;

            var dbContext = new DataContext(options);
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();

			_userRepository = new UserRepository(dbContext);
			_roleRepository = new RoleRepository(dbContext);
			_deviceRepository = new DeviceRepository(dbContext);
			_deviceLogRepository = new DeviceLogRepository(dbContext);

		}
	}
}
