﻿using Domain.SharedKernel.Exceptions;
using Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain.SharedKernel.Validation
{
    /// <summary>
    /// Classe que realiza validações em sequencia.
    /// </summary>
    public class Guard
    {
        private IList<FieldValidationInfo> validations;

        public Guard()
        {
            validations = new List<FieldValidationInfo>();
        }

        /// <summary>
        /// Executa a validação
        /// </summary>
        /// <param name="message">Mensagem a ser devolvida caso sejam encontrados valores inválidos</param>
        public void Validate(string message = "Um ou mais valores informados são inválidos.")
        {
            if (validations.Any(x => !x.IsValid))
                throw new FieldsValidationException(message, validations.Where(c => !c.IsValid).ToList());
        }

        /// <summary>
        /// Realiza apenas a checagem se existe algo inválido, sem lançar exceçao
        /// </summary>
        /// <returns>True caso os dados sejam válidos</returns>
        public bool Check()
        {
            return !validations.Any(x => !x.IsValid);
        }

        public Guard NotNull(string field, object obj)
        {
            if (obj == null)
                validations.Add(new FieldValidationInfo(field, $"O campo {field} é obrigatório", false));
            else
                validations.Add(ValidationOK());

            return this;
        }

        public Guard NotNullOrEmpty(string field, string value)
        {
            if (string.IsNullOrEmpty(value))
                validations.Add(new FieldValidationInfo(field, $"O campo {field} não pode ser vazio", false));
            else
                validations.Add(ValidationOK());

            return this;
        }

        public Guard GreaterThan(string field, IComparable number, IComparable greaterThanNumber)
        {
            if (Comparer.GetComparisonResult(number, greaterThanNumber) > 0)
                validations.Add(ValidationOK());
            else
                validations.Add(new FieldValidationInfo(field, $"O campo {field} deve ser maior do que {greaterThanNumber}.", false));

            return this;

        }

        public Guard LessThan(string field, IComparable number, IComparable lessThanNumber)
        {
            if (Comparer.GetComparisonResult(number, lessThanNumber) < 0)
                validations.Add(ValidationOK());
            else
                validations.Add(new FieldValidationInfo(field, $"O campo {field} deve ser menor do que {lessThanNumber}.", false));

            return this;

        }

        public Guard ValidEmail(string field, string email)
        {
            string expression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

            var regex = new Regex(expression, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(email))
                validations.Add(new FieldValidationInfo(field, "O email não é válido.", false));
            else
                validations.Add(ValidationOK());

            return this;
        }

        public Guard ValidPassword(string field, string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                validations.Add(new FieldValidationInfo(field, "A senha deve ter ao menos 8 caracteres", false));
            else
                validations.Add(ValidationOK());

            return this;
        }

        public Guard HasMoreThanOne<T>(string field, IList<T> lista)
        {
            if(lista == null || lista.Count == 0)
                validations.Add(new FieldValidationInfo(field, $"A lista {field} não pode estar vazia", false));
            else
                validations.Add(ValidationOK());

            return this;
        }

        public Guard ValidDomain(string field, string domain)
        {
            string expression = @"^(([a-zA-Z]{1})|([a-zA-Z]{1}[a-zA-Z]{1})|([a-zA-Z]{1}[0-9]{1})|([0-9]{1}[a-zA-Z]{1})|([a-zA-Z0-9][a-zA-Z0-9-_]{1,61}[a-zA-Z0-9]))\.([a-zA-Z]{2,6}|[a-zA-Z0-9-]{2,30}\.[a-zA-Z]{2,3})$";

            var regex = new Regex(expression, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(domain))
                validations.Add(new FieldValidationInfo(field, "O domínio não é válido.", false));
            else
                validations.Add(ValidationOK());

            return this;
        }


        private FieldValidationInfo ValidationOK()
        {
            return new FieldValidationInfo("", "", true);
        }

    }
}
