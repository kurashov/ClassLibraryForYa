using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibraryForYa.Contracts;
using ClassLibraryForYa.Exceptions;

namespace ClassLibraryForYa.Services
{
    /// <summary>
    /// <inheritdoc cref="IGetNumbersService"/>
    /// </summary>
    public class GetNumbersService : IGetNumbersService
    {
        private readonly ConcurrentDictionary<int, Range> _dictionary;
        private readonly object _lockObject;
        private readonly object _lockObjectForGetNumbers;

        public GetNumbersService()
        {
            _dictionary = new ConcurrentDictionary<int, Range>();
            _lockObject = new object();
            _lockObjectForGetNumbers = new object();
        }

        public int CreateRange( int minValue, int maxValue )
        {
            if( maxValue <= minValue )
            {
                throw new ArgumentException( "incorrect max value", nameof(maxValue) );
            }

            var newId = 0;

            lock( _lockObject )
            {
                if( _dictionary.Count != 0 )
                {
                    newId = _dictionary.Select(d => d.Key).Max() + 1;
                }
                
                var result = _dictionary.TryAdd(newId, new Range(minValue, maxValue));

                if (!result)
                {
                    throw new Exception( "Error occurred during add new range" );
                }
            }

            return newId;
        }

        public int GetNumber( int rangeId )
        {
            if( !_dictionary.ContainsKey( rangeId ) )
            {
                throw new ArgumentException();
            }

            int result = 0;
            bool found = false;

            var range = _dictionary[rangeId];

            lock( range )
            {
                for( int i = range.MinValue; i <= range.MaxValue; i++ )
                {
                    if( range.UsedNumbers.Contains( i ) )
                    {
                        continue;
                    }
                    range.UsedNumbers.Add( i );
                    found = true;
                    result = i;
                    break;
                }
            }

            if( !found )
            {
                throw new NotAvailableValuesInRangeException();
            }

            return result;
        }

        public void FreeNumber( int value, int rangeId )
        {
            throw new NotImplementedException();
        }
    }
}
