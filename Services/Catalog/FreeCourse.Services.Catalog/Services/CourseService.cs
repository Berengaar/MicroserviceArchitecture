using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        #region Without_OptionsPattern
        /*
            private readonly IMapper _mapper;
            private readonly IMongoCollection<Course> _courseCollection;
            private readonly IConfiguration _configuration;

            public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IConfiguration configuration)
            {
                _configuration = configuration;
                var strClient = _configuration.GetSection("DatabaseSettings:ConnectionString").Value;
                var strDatabase = _configuration.GetSection("DatabaseSettings:DatabaseName").Value;

                var client = new MongoClient(strClient);
                var database = client.GetDatabase(strDatabase);
                _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
                _mapper = mapper;
                _configuration = configuration;
            }
         */
        #endregion

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IMongoCollection<Category> categoryCollection)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }
            var mappedCourses = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(data: mappedCourses, 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(c => c.Id == id).FirstOrDefaultAsync();

            if (course != null)
            {
                course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstAsync();
                var mappedCourse = _mapper.Map<CourseDto>(course);
                return Response<CourseDto>.Success(mappedCourse, 200);
            }

            else
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(course => course.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            var mappedCourses = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(data: mappedCourses, 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var mappedCourse = _mapper.Map<Course>(courseCreateDto);
            mappedCourse.CreatedTime = DateTime.Now;

            await _courseCollection.InsertOneAsync(mappedCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(courseCreateDto), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var mappedCourse = _mapper.Map<Course>(courseUpdateDto);

            var updatedCourse = await _courseCollection.FindOneAndReplaceAsync(course => course.Id == courseUpdateDto.Id, mappedCourse);

            if (updatedCourse != null)
            {
                return Response<NoContent>.Success("Course updated successfully", 200);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(course => course.Id == course.Id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success("Course deleted successfully", 204);
            }
            else return Response<NoContent>.Fail("Course not found", 404);
        }

    }
}
