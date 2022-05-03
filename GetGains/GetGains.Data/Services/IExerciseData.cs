﻿using GetGains.Core.Models.Exercises;

namespace GetGains.Data.Services;

public interface IExerciseData
{
    List<Exercise> GetAll();

    Exercise? GetById(int id);

    Exercise Add(Exercise exercise);

    bool Update(Exercise exercise);

    bool Delete(Exercise exercise);
}