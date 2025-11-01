using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace HabbitIt.Application.DataBase;

public class Db : DbContext
{
    public  DbSet<Habit> Habits { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public Db(DbContextOptions<Db> options) : base(options)
    { }


}