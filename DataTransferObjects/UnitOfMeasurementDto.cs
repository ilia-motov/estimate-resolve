﻿using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных единицы измерения.
    /// </summary>
    public class UnitOfMeasurementDto : IDto
    {
        /// <summary>
        /// Получает идентификатор единицы измерения.
        /// </summary>
        public int Id { get; set;}

        /// <summary>
        /// Получает или изменяет имя единицы измерения.
        /// </summary>
        public string Name { get; set;}
    }
}
