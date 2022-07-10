using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsTask.SharedKernal.OperationResults
{
    public class OperationResult<TEnumResult> where TEnumResult : struct, IConvertible
    {
        public OperationResult(TEnumResult enumResult = default, Exception exception = null)
        {
            EnumResult = enumResult;
            Exception = exception;

            if (Exception != null)
            {
                AddError(exception.Message);
            }
        }

        public ICollection<ValidationResult> ValidationResults { get; set; }
        public string ErrorMessages
        {
            get
            {
                if (ValidationResults != null)
                {
                    return string.Join(", ", ValidationResults);
                }
                return string.Empty;
            }
        }
        public TEnumResult EnumResult { get; set; }
        public virtual OperationResult<TEnumResult> AddError(string errorMessage)
        {
            if (ValidationResults is null)
            {
                ValidationResults = new List<ValidationResult>();
            }
            ValidationResults.Add(new ValidationResult(errorMessage));
            return this;
        }
        public Exception Exception { get; protected set; }
    }
    public class OperationResult<TEnumResult, TEntity> : OperationResult<TEnumResult>
        where TEnumResult : struct, IConvertible
    {
        public TEntity Result { get; set; }
        public OperationResult(TEnumResult enumResult = default,
            TEntity entity = default, Exception exception = null)
            : base(enumResult, exception)
        {
            Result = entity;
        }
        public new OperationResult<TEnumResult, TEntity> AddError(string errorMessage)
        {
            base.AddError(errorMessage);
            return this;
        }
    }
    public static class OperationResultExtensions
    {
        public static OperationResult<TEnumResult> UpdateResultStatus<TEnumResult>
            (this OperationResult<TEnumResult> operation, TEnumResult enumResult) where TEnumResult : struct, IConvertible
        {
            operation.EnumResult = enumResult;
            return operation;
        }

        public static OperationResult<TEnumResult, TEntity> UpdateResultStatus<TEnumResult, TEntity>
                (this OperationResult<TEnumResult, TEntity> operation, TEnumResult enumResult) where TEnumResult : struct, IConvertible
        {
            operation.EnumResult = enumResult;
            return operation;
        }
        public static OperationResult<TEnumResult, TEntity> UpdateResultData<TEnumResult, TEntity>
                (this OperationResult<TEnumResult, TEntity> operation, TEntity result) where TEnumResult : struct, IConvertible
        {
            operation.Result = result;
            return operation;
        }

    }
}
