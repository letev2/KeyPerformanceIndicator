﻿namespace KpiNew.Dto
{
    public class BaseRespond<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; } 
    }
}
