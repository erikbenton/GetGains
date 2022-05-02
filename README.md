# GetGains

GetGains is an asp.net core 6 database for creating exercises and building workouts with them.

## Current Goals

* Make a simple CRUD app for "Exercises" to later be used in a "Workout."
* Implement "Exercise Instructions" to each "Exercise" that can be edited.
* Use simple In Memory storage until "Exercise Instructions" are implemented, then move to MSSQL for on-machine development.

## Hopeful Future Goals

* Implement a CRUD SQL database following MVC model.
* Implement a complementary CRUD API.
* Implement Authentication/Authorization.
* Create hosted SPA for running/logging a "Workout" (most likely React).
  * Ideally, a User will create exercises and build a workout with them in the MVC database, then access the hosted "GetGains SPA" for logging the workout.