using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigNumbers
{

    class BigNum
    {
        // The term "this", is "bigNum1"

        private sbyte[] numberArray = new sbyte[10000]; // Main array. size (length) of array can be changed, but is limited to memory of local system
        private int decimalAccurancy;
        private int NumberOfDecimalPlaces { get; set; }
        private bool _isNegative;
        private bool IsNegative
        {
            set
            {
                if (value == true)
                {
                    if (this._isNegative)
                    {
                        _isNegative = false; // For example: -(-123) = +123
                    }
                    else
                    {
                        _isNegative = true; // For example: -(123) = -123
                    }
                }
                else // if value == false
                {
                    _isNegative = value;
                }
            }

            get
            {
                return _isNegative;
            }
        }

        #region Constructors
        public BigNum()
        {
            sbyte[] array = new sbyte[numberArray.Length]; // zero array

            numberArray = array; // 0
        }
        public BigNum(string bigNum)
        {
            Input(bigNum);
        }
        private BigNum(sbyte[] array)
        {
            numberArray = array;
        }
        public BigNum(BigNum bigNum)
        {
            Input(bigNum.ToString());
        }
        #endregion

        private void Input(string input)
        {
            if (IsValid(input)) // validate first of all
            {
                if (input.Contains('E'))
                {
                    input = ConvertToNum(input);
                }

                input = Simplify(input); // simplify after validation
                input = ToInteger(input); // convert to integer after simlifying

                char[] charArray = input.ToCharArray();

                for (int numberArrayIndex = numberArray.Length - charArray.Length, charArrayIndex = 0; charArrayIndex < charArray.Length; numberArrayIndex++, charArrayIndex++)
                {
                    numberArray[numberArrayIndex] = sbyte.Parse(charArray[charArrayIndex].ToString());
                }
            }
            else // if input string was not valid
            {
                throw new ArgumentException("Input number was not in a correct format.");
            }
        }

        public string ConvertToNum(string scientificNotation)
        {
            BigNum resBigNum = new BigNum(scientificNotation.Substring(0, scientificNotation.IndexOf('E')));

            int powerOfTen = int.Parse(scientificNotation.Substring(scientificNotation.IndexOf('E') + 1));
            if (powerOfTen > 0)
            {
                resBigNum = resBigNum.MulTen(powerOfTen);
            }
            else if (scientificNotation[scientificNotation.IndexOf('E') + 1] == '-')
            {
                BigNum r = new BigNum();
                resBigNum = resBigNum.DivTen(Math.Abs(powerOfTen));
            }

            return resBigNum.ToString();
        }

        private bool IsValid(string input) // validate first of all
        {
            if (GetNumberOfPoints(input) > 1)
            {
                return false; // Invalid
            }

            // if "+" or "-" is not at the first, its invalid
            // for example "12+3456" or "123-456" is not valid
            if (input.LastIndexOf('-') > 0 || input.LastIndexOf('+') > 0)
            {
                if (input.Contains('E'))
                {
                    if (!(input[input.IndexOf('E') + 1] == '+' || input[input.IndexOf('E') + 1] == '-'))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }


            for (int i = 0; i < input.Length; i++)
            {
                // only numbers(1, 2, ..., 9) and "." and "+" and "-" are allowed
                if (input[i] == '0' || input[i] == '1' || input[i] == '2' || input[i] == '3' || input[i] == '4' || input[i] == '5' || input[i] == '6' || input[i] == '7' || input[i] == '8' || input[i] == '9' || input[i] == '.' || input[i] == '+' || input[i] == '-' || input[i] == 'E')
                {
                    // Valid. Do nothing.
                }
                else
                {
                    return false; // Invalid
                }
            }

            return true;
        }

        private int GetNumberOfPoints(string input)
        {
            int pointCounter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    pointCounter++;
                }
            }

            return pointCounter;
        }

        private string Simplify(string input) // simplify after validation
        {
            // convert input string to charList
            List<char> charList = input.ToCharArray().ToList();

            // remove "+"
            // for example "+123" ===> "123"
            if (charList[0] == '+')
            {
                charList.RemoveAt(0);
            }

            // if "." is at the beginnig, insert 0 before "."
            // for example: ".123" ===> "0.123"
            if (charList[0] == '-' || charList[0] == '+')
            {
                if (charList[1] == '.')
                {
                    charList.Insert(1, '0');
                }
            }
            else if (charList[0] == '.')
            {
                charList.Insert(0, '0');
            }

            // if "." is at the end, remove "."
            // for example "123." ===> "123"
            if (charList[charList.Count - 1] == '.')
            {
                charList.Remove('.');
            }

            // remove unnecessary zeros
            // for example "000123.456000" ===> "123.456"
            // for example "00012345600.0" ===> "12345600"
            RemoveLeftZeros(charList);
            RemoveRightZeros(charList);

            // if charList is empty, add '0'
            // "" ===> "0"
            if (charList.Count == 0)
            {
                charList.Add('0');
            }

            // if its "-0" then remove "-"
            //"-0" ===> "0"
            if (charList.Count == 2 && charList[0] == '-' && charList[1] == '0')
            {
                charList.RemoveAt(0);
            }

            // convert charList to string
            string result = string.Empty;
            for (int i = 0; i < charList.Count; i++)
            {
                result += charList[i].ToString();
            }

            return result;
        }

        private void RemoveLeftZeros(List<char> input)
        {
            if (input.Contains('.'))
            {
                for (int i = (input[0] == '-' ? 1 : 0); i < input.Count; i++)
                {
                    if (input[i] == '0' && i + 1 < input.Count && input[i + 1] != '.')
                    {
                        input.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = (input[0] == '-' ? 1 : 0); i < input.Count; i++)
                {
                    if (input[i] == '0')
                    {
                        input.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void RemoveRightZeros(List<char> input)
        {
            if (input.Contains('.'))
            {
                for (int i = input.Count - 1; i >= 0; i = input.Count - 1)
                {
                    if (input[i] == '0')
                    {
                        input.RemoveAt(input.LastIndexOf('0'));

                        if (i - 1 >= 0 && input[i - 1] == '.')
                        {
                            input.Remove('.');
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private string ToInteger(string decimalInput) // convert to integer after simlifying
        {
            string intResult = string.Empty;
            NumberOfDecimalPlaces = 0;

            // if its a negative number
            // for example "-123.456"
            if (decimalInput[0] == '-')
            {
                IsNegative = true;
            }

            for (int i = decimalInput[0] == '-' || decimalInput[0] == '+' ? 1 : 0; i < decimalInput.Length; i++)
            {
                if (decimalInput[i] != '.') // if it's just a number (0, 1, 2, ... , 9)
                {
                    intResult += decimalInput[i]; // add the number to result
                }
                else // if there is any "."
                {
                    NumberOfDecimalPlaces = decimalInput.Length - 1 - i; // set number of decimal places
                                                                         // dont add point to result
                }
            }

            return intResult;
        }

        public override string ToString()
        {
            string result = string.Empty;

            // if all of the numbers in main array is 0, return "0"
            if (this.GetFirstDigitIndex() == numberArray.Length)
            {
                result = "0";
            }
            else
            {
                for (int i = numberArray.Length - GetDigitCount(); i < numberArray.Length; i++)
                {
                    result += numberArray[i].ToString();
                }

                if (NumberOfDecimalPlaces > 0) // if it is a decimal number
                {
                    int indexOfPoint = result.Length - NumberOfDecimalPlaces;

                    if (indexOfPoint < 0)
                    {
                        for (int i = indexOfPoint; i < 0; i++)
                        {
                            result = "0" + result;
                            indexOfPoint++;
                        }

                        //indexOfPoint = 0;
                    }

                    result = result.Insert(indexOfPoint, "."); // insert point

                    // if the point inserted to the beginning 
                    // insert "0" to the beginning
                    // for example: ".123" ===> "0.123"
                    if (indexOfPoint == 0)
                    {
                        result = "0" + result;
                    }
                }

                // if it is a negative number, add "-" to the beginning
                // for example: "1234" ===> "-1234"
                if (IsNegative)
                {
                    result = '-' + result;
                }
            }

            return Simplify(result);
        }

        public BigNum Add(BigNum bigNum2)
        {
            BigNum result = new BigNum();
            int largestNumberOfDecimalPlaces = 0;

            BigNum integeredBigNum1 = new BigNum();
            BigNum integeredBigNum2 = new BigNum();


            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0) // if any of bigNums are decimal
            {
                // store the largest number of decimal places of bigNum1 and bigNum2
                // For example: bigNum1 = 12.34 and bigNum2 = 5.678 ===> largestNumberOfDecimalPlaces = 3
                largestNumberOfDecimalPlaces = Math.Max(this.NumberOfDecimalPlaces, bigNum2.NumberOfDecimalPlaces);

                // multiplicate decimal numbers to a power of 10 to eliminate the floating point
                integeredBigNum1 = this.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces - this.NumberOfDecimalPlaces).ToString()));
                integeredBigNum1.NumberOfDecimalPlaces = 0;
                integeredBigNum2 = bigNum2.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces - bigNum2.NumberOfDecimalPlaces).ToString()));
                integeredBigNum2.NumberOfDecimalPlaces = 0;
            }
            else
            {
                integeredBigNum1 = this;
                integeredBigNum2 = bigNum2;
            }

            if (!integeredBigNum1.IsNegative && !integeredBigNum2.IsNegative) // if both of the numbers are positive
            {
                // do normal work
                sbyte[] resultArray = new sbyte[integeredBigNum1.numberArray.Length];

                for (int i = integeredBigNum1.numberArray.Length - 1; i >= 0; i--)
                {
                    resultArray[i] += (sbyte)(integeredBigNum1.numberArray[i] + integeredBigNum2.numberArray[i]);

                    if (resultArray[i] >= 10)
                    {
                        resultArray[i] -= 10;
                        resultArray[i - 1] += 1;
                    }
                }

                result = new BigNum(resultArray);
            }
            else if (integeredBigNum1.IsNegative && integeredBigNum2.IsNegative) // if both of the numbers are negative.
            {
                BigNum positiveBigNum1 = new BigNum(integeredBigNum1);
                positiveBigNum1.IsNegative = false; // change bigNum1 to positive

                BigNum positiveBigNum2 = new BigNum(integeredBigNum2);
                positiveBigNum2.IsNegative = false; // change bigNum2 to positive

                result = positiveBigNum1.Add(positiveBigNum2); //  For example: (-123) + (-456) = -123 - 456 = -(123 + 456)
                result.IsNegative = true;
            }
            else if (integeredBigNum1.IsNegative) // if bigNum1 < 0 and bigNum2 > 0
            {
                BigNum positiveBigNum1 = new BigNum(integeredBigNum1);
                positiveBigNum1.IsNegative = false; // change bigNum1 to positive

                result = integeredBigNum2.Subtract(positiveBigNum1); // For example: (-123) + (+456) = 456 - 123
            }
            else if (integeredBigNum2.IsNegative) // if bigNum1 > 0 and bigNum2 < 0
            {
                BigNum positiveBigNum2 = new BigNum(integeredBigNum2);
                positiveBigNum2.IsNegative = false; // change bigNum2 to positive

                result = integeredBigNum1.Subtract(positiveBigNum2); // For example: (+123) + (-456) = 123 - 456
            }

            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0) // if any of bigNums were decimal
            {
                result.NumberOfDecimalPlaces = largestNumberOfDecimalPlaces; // recover decimal point
            }

            return result;
        }

        public BigNum Subtract(BigNum bigNum2)
        {
            BigNum result = new BigNum();
            int largestNumberOfDecimalPlaces = 0;

            BigNum integeredBigNum1;
            BigNum integeredBigNum2;


            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0) // if any of bigNums are decimal
            {
                // store the largest number of decimal places of bigNum1 and bigNum2
                // For example: bigNum1 = 12.34 and bigNum2 = 5.678 ===> largestNumberOfDecimalPlaces = 3
                largestNumberOfDecimalPlaces = Math.Max(this.NumberOfDecimalPlaces, bigNum2.NumberOfDecimalPlaces);

                // multiplicate decimal numbers to a power of 10 to eliminate the floating point
                integeredBigNum1 = this.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces - this.NumberOfDecimalPlaces).ToString()));
                integeredBigNum1.NumberOfDecimalPlaces = 0;
                integeredBigNum2 = bigNum2.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces - bigNum2.NumberOfDecimalPlaces).ToString()));
                integeredBigNum2.NumberOfDecimalPlaces = 0;
            }
            else
            {
                integeredBigNum1 = new BigNum(this);
                integeredBigNum2 = new BigNum(bigNum2);
            }

            if (!integeredBigNum1.IsNegative && !integeredBigNum2.IsNegative) // if both of the numbers are positive
            {
                if (integeredBigNum1.IsGreaterThanOrEqual(integeredBigNum2)) // if bigNum1 >= bigNum2. for example: 456 > 123 ===> 456 - 123
                {
                    // do normal work
                    sbyte[] resultArray = new sbyte[integeredBigNum1.numberArray.Length];

                    for (int i = integeredBigNum1.numberArray.Length - 1; i >= 0; i--)
                    {
                        resultArray[i] += (sbyte)(integeredBigNum1.numberArray[i] - integeredBigNum2.numberArray[i]);

                        if (resultArray[i] < 0)
                        {
                            resultArray[i] += 10;
                            resultArray[i - 1] -= 1;
                        }
                    }

                    result = new BigNum(resultArray);
                }
                else // if bigNum1 < bigNum2. for example: 123 < 456
                {
                    result = integeredBigNum2.Subtract(integeredBigNum1); // for example: 123 - 456 = -(456 - 123) 
                    result.IsNegative = true;
                }
            }
            else if (integeredBigNum1.IsNegative && integeredBigNum2.IsNegative) // if both bigNums is negative
            {
                BigNum positiveBigNum1 = new BigNum(integeredBigNum1);
                positiveBigNum1.IsNegative = false; // change bigNum1 to positive

                BigNum positiveBigNum2 = new BigNum(integeredBigNum2);
                positiveBigNum2.IsNegative = false; // change bigNum2 to positive 

                result = positiveBigNum2.Subtract(positiveBigNum1); // For example: (-123) - (-456) = -123 + 456 = 456 - 123
            }
            else if (integeredBigNum2.IsNegative) // if bigNum1 > 0 and bigNum2 < 0
            {
                BigNum positiveBigNum2 = new BigNum(integeredBigNum2);
                positiveBigNum2.IsNegative = false; // change bigNum2 to positive 

                result = integeredBigNum1.Add(positiveBigNum2); // for example: 123 - (-456) = 123 + 456
            }
            else if (integeredBigNum1.IsNegative) // if bigNum1 < 0 and bigNum2 > 0
            {
                BigNum positiveBigNum1 = new BigNum(integeredBigNum1);
                positiveBigNum1.IsNegative = false; // change bigNum1 to positive

                result = positiveBigNum1.Add(integeredBigNum2); // for example: (-123) - 456 = -(123 + 456)
                result.IsNegative = true;
            }

            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0) // if any of bigNums were decimal
            {
                result.NumberOfDecimalPlaces = largestNumberOfDecimalPlaces; // recover decimal point
            }

            return result;
        }

        public BigNum Multiplication(BigNum bigNum2)
        {
            BigNum result = new BigNum();
            int sumOfNumberOfDecimalPlaces = this.NumberOfDecimalPlaces + bigNum2.NumberOfDecimalPlaces;

            if ((!this.IsNegative && !bigNum2.IsNegative) || (this.IsNegative && bigNum2.IsNegative)) // if both of bigNums is positive or both of them is negative
            {
                // do normal work
                int digitCountOfBigNum1 = this.GetDigitCount();
                int digitCountOfBigNum2 = bigNum2.GetDigitCount();

                for (int index2 = bigNum2.numberArray.Length - 1; index2 >= numberArray.Length - digitCountOfBigNum2; index2--)
                {
                    sbyte[] resultArray = new sbyte[numberArray.Length];
                    int indexResult = resultArray.Length - 1;

                    for (int i = resultArray.Length - 1; i >= index2 + 1; i--)
                    {
                        resultArray[indexResult] = 0;
                        indexResult--;
                    }

                    for (int index1 = this.numberArray.Length - 1; index1 >= numberArray.Length - digitCountOfBigNum1; index1--, indexResult--)
                    {
                        resultArray[indexResult] += (sbyte)(bigNum2.numberArray[index2] * this.numberArray[index1]);
                        if (resultArray[indexResult] >= 10)
                        {
                            resultArray[indexResult - 1] += (sbyte)(resultArray[indexResult] / 10);
                            resultArray[indexResult] %= 10;
                        }
                    }

                    result = result.Add(new BigNum(resultArray));
                }
            }
            else if (this.IsNegative ^ bigNum2.IsNegative) // if JUST one of the bigNums is true
            {
                BigNum positiveBigNum1 = new BigNum(this);
                positiveBigNum1.IsNegative = false; // change bigNum1 to positive

                BigNum positiveBigNum2 = new BigNum(bigNum2);
                positiveBigNum2.IsNegative = false; // change bigNum2 to positive 

                result = positiveBigNum1.Multiplication(positiveBigNum2); // For example: (-123) * (+456) = -(123 * 456)
                result.IsNegative = true;
            }

            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0) // if any of bigNums were decimal
            {
                result.NumberOfDecimalPlaces = sumOfNumberOfDecimalPlaces; // recover decimal point
            }

            return result;
        }

        public BigNum Division(BigNum bigNum2, out BigNum remainder, int numberOfDecimalPlaces)
        {
            if (bigNum2.IsZero())
            {
                throw new DivideByZeroException();
            }

            decimalAccurancy = numberOfDecimalPlaces;
            BigNum result = new BigNum();
            remainder = new BigNum();

            int largestNumberOfDecimalPlaces = 0;

            BigNum integeredBigNum1 = new BigNum(this);
            BigNum integeredBigNum2 = new BigNum(bigNum2);

            if (this.NumberOfDecimalPlaces > 0 || bigNum2.NumberOfDecimalPlaces > 0)
            {
                largestNumberOfDecimalPlaces = Math.Max(this.NumberOfDecimalPlaces, bigNum2.NumberOfDecimalPlaces);

                integeredBigNum1 = integeredBigNum1.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces).ToString()));
                integeredBigNum2 = integeredBigNum2.Multiplication(new BigNum(Math.Pow(10, largestNumberOfDecimalPlaces).ToString()));
            }

            result = integeredBigNum1.BasicDivision(integeredBigNum2, out remainder);

            if (!remainder.IsZero())
            {
                result = GetDecimalDiv(integeredBigNum2, result, remainder);
            }

            remainder.NumberOfDecimalPlaces = largestNumberOfDecimalPlaces;

            if (integeredBigNum1.IsNegative && integeredBigNum2.IsNegative)
            {
                remainder.IsNegative = true;
            }
            else if (integeredBigNum1.IsNegative && !integeredBigNum2.IsNegative)
            {
                remainder.IsNegative = true;
            }

            if ((!integeredBigNum1.IsNegative && !integeredBigNum2.IsNegative) || (integeredBigNum1.IsNegative && integeredBigNum2.IsNegative)) // if both of bigNums is positive or both of them is negative
            {
                result.IsNegative = false;
            }
            else if (integeredBigNum1.IsNegative ^ integeredBigNum2.IsNegative) // if JUST one of the bigNums is true
            {
                result.IsNegative = true;
            }

            return result;
        }

        private BigNum BasicDivision(BigNum bigNum2, out BigNum remainder)
        {
            BigNum result = new BigNum();

            BigNum integeredBigNum1 = new BigNum(this);
            integeredBigNum1.NumberOfDecimalPlaces = 0;
            integeredBigNum1.IsNegative = false;

            BigNum integeredBigNum2 = new BigNum(bigNum2);
            integeredBigNum2.NumberOfDecimalPlaces = 0;
            integeredBigNum2.IsNegative = false;

            remainder = new BigNum(integeredBigNum1);

            while (remainder.IsGreaterThanOrEqual(integeredBigNum2) && !remainder.IsZero())
            {
                // calulate the division of first digits:
                int intInitialQuotient = remainder.numberArray[remainder.GetFirstDigitIndex()] / integeredBigNum2.numberArray[integeredBigNum2.GetFirstDigitIndex()];
                BigNum initialQuotient = new BigNum(intInitialQuotient.ToString());
                if (initialQuotient.IsZero()) // if the calculation result of division of first digits were zero
                {
                    // calulate the division of first two digits of bigNum1 and first digit of bigNum2
                    intInitialQuotient = (remainder.numberArray[remainder.GetFirstDigitIndex()] * 10 + remainder.numberArray[remainder.GetFirstDigitIndex() + 1]) / integeredBigNum2.numberArray[integeredBigNum2.GetFirstDigitIndex()];
                    initialQuotient = new BigNum(intInitialQuotient.ToString());
                }
                BigNum mulResult = initialQuotient.Multiplication(integeredBigNum2);
                // number of zeros to be added to the right side of mulResult and quotient
                BigNum tenPowerNumberOfDigitsDiffrence = new BigNum((Math.Pow(10, remainder.GetDigitCount() - integeredBigNum2.GetDigitCount()).ToString()));
                mulResult = mulResult.Multiplication(tenPowerNumberOfDigitsDiffrence);
                BigNum quotient = initialQuotient.Multiplication(tenPowerNumberOfDigitsDiffrence);

                while (remainder.IsLessThan(mulResult))
                {
                    initialQuotient = initialQuotient.Subtract(new BigNum("1"));
                    if (initialQuotient.IsZero())
                    {
                        initialQuotient = new BigNum("1");
                        mulResult = initialQuotient.Multiplication(integeredBigNum2);
                        tenPowerNumberOfDigitsDiffrence = new BigNum((Math.Pow(10, remainder.GetDigitCount() - mulResult.GetDigitCount() - 1).ToString()));
                    }
                    else
                    {
                        mulResult = initialQuotient.Multiplication(integeredBigNum2);
                        tenPowerNumberOfDigitsDiffrence = new BigNum((Math.Pow(10, remainder.GetDigitCount() - mulResult.GetDigitCount()).ToString()));
                    }

                    mulResult = mulResult.Multiplication(tenPowerNumberOfDigitsDiffrence);
                    quotient = initialQuotient.Multiplication(tenPowerNumberOfDigitsDiffrence);
                }

                result = result.Add(quotient);
                remainder = remainder.Subtract(mulResult);
            }

            return result;
        }

        private BigNum GetDecimalDiv(BigNum bigNum2, BigNum quotient, BigNum remainder)
        {
            BigNum result = new BigNum();
            BigNum integeredBigNum2 = new BigNum(bigNum2);
            integeredBigNum2.IsNegative = false;

            string decimalResult = quotient.ToString();
            decimalResult += '.';

            while (!remainder.IsZero())
            {
                for (int decimalPlaceCounter = 0; decimalPlaceCounter < decimalAccurancy; decimalPlaceCounter++)
                {
                    remainder = remainder.Multiplication(new BigNum(Math.Pow(10, 1).ToString()));
                    if (remainder.IsLessThan(integeredBigNum2))
                    {
                        remainder = remainder.Multiplication(new BigNum(Math.Pow(10, 1).ToString()));
                        decimalResult += '0';
                        decimalPlaceCounter++;
                    }

                    BigNum r2 = new BigNum();
                    BigNum quo = remainder.BasicDivision(integeredBigNum2, out r2);
                    decimalResult += quo.ToString()[0];
                    BigNum mulRes = quo.Multiplication(integeredBigNum2);
                    remainder = remainder.Subtract(mulRes);
                }

                break;
            }

            result = new BigNum(decimalResult);

            return result;
        }

        private BigNum BasicDivision2(BigNum bigNum2, out BigNum remainder)
        {
            BigNum result = new BigNum();

            BigNum integeredBigNum1 = new BigNum(this);
            BigNum integeredBigNum2 = new BigNum(bigNum2);

            remainder = new BigNum(this);

            while (remainder.IsGreaterThanOrEqual(integeredBigNum2) && !remainder.IsZero())
            {
                result = result.Add(new BigNum("1"));
                remainder = remainder.Subtract(integeredBigNum2);
            }

            return result;
        }

        public static BigNum Power(BigNum baseNum, int powNum)
        {
            BigNum result = new BigNum("1");

            for (int i = 0; i < powNum; i++)
            {
                result = result.Multiplication(baseNum);
            }

            return result;
        }

        private BigNum GetSimple()
        {
            int firstDigitIndex = this.GetFirstDigitIndex();
            int zeroCountAfterFirstDigit = (this.numberArray.Length - 1) - firstDigitIndex;

            string num = this.numberArray[firstDigitIndex].ToString();
            for (int zeroCounter = 0; zeroCounter < zeroCountAfterFirstDigit; zeroCounter++)
            {
                num += "0";
            }

            return new BigNum(num);
        }

        public int GetDigitCount()
        {
            for (int i = 0; i < numberArray.Length; i++)
            {
                if (numberArray[i] != 0)
                {
                    return numberArray.Length - i;
                }
            }

            return 0;
        }

        public bool IsEqualTo(BigNum bigNum2)
        {
            if (this.GetDigitCount() != bigNum2.GetDigitCount())
            {
                return false;
            }
            else
            {
                for (int i = this.numberArray.Length - this.GetDigitCount(); i < this.numberArray.Length; i++)
                {
                    if (this.numberArray[i] != bigNum2.numberArray[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsNonEqualTo(BigNum bigNum2)
        {
            return !this.IsEqualTo(bigNum2);
        }

        public bool IsGreaterThan(BigNum bigNum2)
        {
            if (this.GetDigitCount() > bigNum2.GetDigitCount())
            {
                return true;
            }
            else if (this.GetDigitCount() < bigNum2.GetDigitCount())
            {
                return false;
            }
            else // have equal digit count
            {
                for (int i = this.numberArray.Length - this.GetDigitCount(); i < this.numberArray.Length; i++)
                {
                    if (this.numberArray[i] > bigNum2.numberArray[i])
                    {
                        return true;
                    }
                    else if (this.numberArray[i] < bigNum2.numberArray[i])
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public bool IsGreaterThanOrEqual(BigNum bigNum2)
        {
            if (this.IsGreaterThan(bigNum2) || this.IsEqualTo(bigNum2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLessThan(BigNum bigNum2)
        {
            return !this.IsGreaterThanOrEqual(bigNum2);
        }

        public bool IsLessThanOrEqual(BigNum BigNum)
        {
            return !this.IsGreaterThan(BigNum);
        }

        public bool IsZero()
        {
            for (int i = 0; i < numberArray.Length; i++)
            {
                if (numberArray[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int GetFirstDigitIndex()
        {
            int i;
            for (i = 0; i < this.numberArray.Length; i++)
            {
                if (this.numberArray[i] != 0)
                {
                    return i;
                }
            }

            return i;
        }

        private BigNum MulTen(int times)
        {
            BigNum result = new BigNum(this);
            BigNum ten = new BigNum("10");

            for (int i = 0; i < times; i++)
            {
                result = result.Multiplication(ten);
            }

            return result;
        }

        private BigNum DivTen(int times)
        {
            BigNum result = new BigNum(this);
            BigNum ten = new BigNum("10");
            BigNum r = new BigNum();

            for (int i = 0; i < times; i++)
            {
                result = result.Division(ten, out r, 1);
            }

            return result;
        }
    }
}

