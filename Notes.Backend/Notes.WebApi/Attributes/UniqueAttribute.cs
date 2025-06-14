using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Persistence;

public class UniqueAttribute : ValidationAttribute
{
    private readonly string _propertyName;

    public UniqueAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success!; // Пустое значение допустимо только если поле необязательно
        }

        var context = (NotesDbContext)validationContext.GetService(typeof(NotesDbContext))!;
        if (context == null)
        {
            return new ValidationResult("Контекст базы данных не найден.");
        }

        var property = validationContext.ObjectType.GetProperty(_propertyName);
        if (property == null)
        {
            return new ValidationResult($"Свойство {_propertyName} не найдено.");
        }

        var entity = validationContext.ObjectInstance;
        var propertyValue = property.GetValue(entity)?.ToString()!.ToLower();

        var isTaken = context.Users.Any(u => EF.Property<string>(u, _propertyName).ToLower() == propertyValue);
        return isTaken ? new ValidationResult($"Значение '{propertyValue}' для {_propertyName} уже занято.") : ValidationResult.Success!;
    }
}