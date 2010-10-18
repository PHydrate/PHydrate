#region Copyright

/*
 * Copyright (c) 2008, Red Gate Software Ltd
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 *
 * * Redistributions of source code must retain the above copyright notice, this list of conditions
 *   and the following disclaimer.
 *
 * * Redistributions in binary form must reproduce the above copyright notice, this list of
 *   conditions and the following disclaimer in the documentation and/or other materials 
 *   provided with the distribution.
 *
 * * Neither the name of Red Gate Software Ltd nor the names of its contributors may be used to
 *   endorse or promote products derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS
 * BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
 * BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UMMO.TestingUtils.RandomData.Waffle
{
    /// <summary>
    /// Generate random text.
    /// </summary>
    /// <remarks>
    /// This class is borrowed from Andrew Clarke from Red Gate
    /// Original article can be found here: http://www.simple-talk.com/dotnet/.net-tools/the-waffle-generator/
    /// </remarks>
    // TODO: Examine this code closely, I suspect that it is fragile.
    public class WaffleEngine
    {
        private readonly IRandom _random;

        private int _cardinalSequence;
        private int _ordinalSequence;
        private string _title;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaffleEngine"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public WaffleEngine( IRandom random )
        {
            _random = random;
        }

        private void EvaluateRandomPhrase( IList< string > phrases, StringBuilder output )
        {
            EvaluatePhrase( phrases[ _random.Next( 0, phrases.Count ) ], output );
        }

        internal void EvaluatePhrase( string phrase, StringBuilder result )
        {
            for ( int i = 0; i < phrase.Length; i++ )
                if ( phrase[ i ] == '|' && i + 1 < phrase.Length )
                {
                    i++;

                    StringBuilder escape = result;
                    bool titleCase = false;

                    if ( phrase[ i ] == 'u' && i + 1 < phrase.Length )
                    {
                        escape = new StringBuilder();
                        titleCase = true;
                        i++;
                    }

                    switch ( phrase[ i ] )
                    {
                        case 'a':
                            EvaluateCardinalSequence( escape );
                            break;
                        case 'b':
                            EvaluateOrdinalSequence( escape );
                            break;
                        case 'c':
                            EvaluateRandomPhrase( WafflePhrases.Buzzphrases, escape );
                            break;
                        case 'd':
                            EvaluateRandomPhrase( WafflePhrases.Verbs, escape );
                            break;
                        case 'e':
                            EvaluateRandomPhrase( WafflePhrases.Adverbs, escape );
                            break;
                        case 'f':
                            EvaluateRandomPhrase( WafflePhrases.Forenames, escape );
                            break;
                        case 's':
                            EvaluateRandomPhrase( WafflePhrases.Surnames, escape );
                            break;
                        case 'o':
                            EvaluateRandomPhrase( WafflePhrases.ArtyNouns, escape );
                            break;
                        case 'y':
                            RandomDate( escape );
                            break;
                        case 'h':
                            EvaluateRandomPhrase( WafflePhrases.Prefixes, escape );
                            break;
                        case 'A':
                            EvaluateRandomPhrase( WafflePhrases.PreamblePhrases, escape );
                            break;
                        case 'B':
                            EvaluateRandomPhrase( WafflePhrases.SubjectPhrases, escape );
                            break;
                        case 'C':
                            EvaluateRandomPhrase( WafflePhrases.VerbPhrases, escape );
                            break;
                        case 'D':
                            EvaluateRandomPhrase( WafflePhrases.ObjectPhrases, escape );
                            break;
                        case '1':
                            EvaluateRandomPhrase( WafflePhrases.FirstAdjectivePhrases, escape );
                            break;
                        case '2':
                            EvaluateRandomPhrase( WafflePhrases.SecondAdjectivePhrases, escape );
                            break;
                        case '3':
                            EvaluateRandomPhrase( WafflePhrases.NounPhrases, escape );
                            break;
                        case '4':
                            EvaluateRandomPhrase( WafflePhrases.Cliches, escape );
                            break;
                        case 't':
                            escape.Append( _title );
                            break;
                        case 'n':
                            escape.Append( "</p>\n<p>" );
                            break;
                    }

                    if ( titleCase )
                        result.Append( TitleCaseWords( escape.ToString() ) );
                }
                else
                    result.Append( phrase[ i ] );
        }

        private void EvaluateCardinalSequence( StringBuilder output )
        {
            if ( _cardinalSequence >= WafflePhrases.CardinalSequence.Length )
                _cardinalSequence = 0;

            output.Append( WafflePhrases.CardinalSequence[ _cardinalSequence++ ] );
        }

        private void EvaluateOrdinalSequence( StringBuilder output )
        {
            if ( _ordinalSequence >= WafflePhrases.OrdinalSequences.Length )
                _ordinalSequence = 0;

            output.Append( WafflePhrases.OrdinalSequences[ _ordinalSequence++ ] );
        }

        private void RandomDate( StringBuilder output )
        {
            output.AppendFormat( "{0:04u}", DateTime.Now.Year - _random.Next( 0, 31 ) );
        }

        private static string TitleCaseWords( string input )
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase( input );
        }

/*
        public void HtmlWaffle( int paragraphs, Boolean includeHeading, StringBuilder result )
        {
            _title = string.Empty;
            _cardinalSequence = 0;
            _ordinalSequence = 0;

            if ( includeHeading )
            {
                var title = new StringBuilder();
                EvaluatePhrase( "the |o of |2 |o", title );

                _title = TitleCaseWords( title.ToString() );

                result.AppendLine( "<html>" );
                result.AppendLine( "<head>" );
                result.AppendFormat( "<title>{0}</title>", _title );
                result.AppendLine();
                result.AppendLine( "</head>" );
                result.AppendLine( "<body>" );
                result.AppendFormat( @"<h1>{0}</h1>", _title );
                result.AppendLine();
                EvaluatePhrase( "<blockquote>\"|A |B |C |t\"<br>", result );
                EvaluatePhrase( "<cite>|f |s in The Journal of the |uc (|uy)</cite></blockquote>", result );
                result.AppendLine();
                EvaluatePhrase( "<h2>|c.</h2>", result );
                result.AppendLine();
            }
            result.Append( "<p>" );

            for ( int i = 0; i < paragraphs; i++ )
            {
                if ( i != 0 )
                    EvaluateRandomPhrase( WafflePhrases.MaybeHeading, result );

                EvaluatePhrase( "|A |B |C |D.  ", result );
                EvaluateRandomPhrase( WafflePhrases.MaybeParagraph, result );
            }

            result.AppendLine( "</p>" );
            result.AppendLine( "</body>" );
            result.AppendLine( "</html>" );
        }
*/

/*
        public void TextWaffle( int paragraphs, Boolean includeHeading, StringBuilder result )
        {
            _title = string.Empty;
            _cardinalSequence = 0;
            _ordinalSequence = 0;

            if ( includeHeading )
            {
                var title = new StringBuilder();
                EvaluatePhrase( "the |o of |2 |o", title );

                _title = TitleCaseWords( title.ToString() );

                result.AppendLine( _title );
                result.AppendLine();
                EvaluatePhrase( "\"|A |B |C |t\"\n", result );
                EvaluatePhrase( "(|f |s in The Journal of the |uc (|uy))", result );
                result.AppendLine();
                EvaluatePhrase( "|c.", result );
                result.AppendLine();
            }

            for ( int i = 0; i < paragraphs; i++ )
            {
                if ( i != 0 )
                    EvaluateRandomPhrase( WafflePhrases.MaybeHeading, result );

                EvaluatePhrase( "|A |B |C |D.  ", result );
                EvaluateRandomPhrase( WafflePhrases.MaybeParagraph, result );
            }
        }
*/
    }
}