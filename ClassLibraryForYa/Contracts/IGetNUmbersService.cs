using System;
using System.Data;
using ClassLibraryForYa.Exceptions;

namespace ClassLibraryForYa.Contracts
{
    /// <summary>
    /// Represent service which can return client number.
    /// </summary>
    public interface IGetNumbersService
    {
        /// <summary>
        /// Create available range.
        /// </summary>
        /// <param name="minValue">min range value.</param>
        /// <param name="maxValue">max range value.</param>
        /// <returns>Range id</returns>
        /// <exception cref="ArgumentException"> Throws if max value less or equal min value.</exception>>
        int CreateRange( int minValue, int maxValue );

        /// <summary>
        /// Return uniq client number.
        /// </summary>
        /// <returns>Client number.</returns>
        /// <exception cref="ArgumentException"> Throws if rangeId does not exist </exception>
        /// <exception cref="NotAvailableValuesInRangeException"> Throws if there are not available values in range </exception>>
        int GetNumber( int rangeId );

        /// <summary>
        /// Free number in range.
        /// </summary>
        /// <param name="value">Value to free.</param>
        /// <param name="rangeId">Range id.</param>
        /// <exception cref="ArgumentException"> Throws if rangeId or value are incorrect.</exception>>
        void FreeNumber( int value, int rangeId );
    }
}
