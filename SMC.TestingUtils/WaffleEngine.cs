using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SMC.TestingUtils
{
    // This class is borrowed from Andrew Clarke from Red Gate
    // Original article can be found here: http://www.simple-talk.com/dotnet/.net-tools/the-waffle-generator/

    public class WaffleEngine
    {
        readonly Random _random;

        int _cardinalSequence;
        int _ordinalSequence;
        string _title;

        public WaffleEngine( Random random )
        {
            _random = random;
        }

        void EvaluateRandomPhrase( IList< string > phrases, StringBuilder output )
        {
            EvaluatePhrase( phrases[_random.Next( 0, phrases.Count )], output );
        }

        internal void EvaluatePhrase( string phrase, StringBuilder result )
        {
            for ( int i = 0; i < phrase.Length; i++ )
            {
                if ( phrase[i] == '|' && i + 1 < phrase.Length )
                {
                    i++;

                    StringBuilder escape = result;
                    bool titleCase = false;

                    if ( phrase[i] == 'u' && i + 1 < phrase.Length )
                    {
                        escape = new StringBuilder();
                        titleCase = true;
                        i++;
                    }

                    switch ( phrase[i] )
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
                    {
                        result.Append( TitleCaseWords( escape.ToString() ) );
                    }
                }
                else
                {
                    result.Append( phrase[i] );
                }
            }
        }

        void EvaluateCardinalSequence( StringBuilder output )
        {
            if ( _cardinalSequence >= WafflePhrases.CardinalSequence.Length )
            {
                _cardinalSequence = 0;
            }

            output.Append( WafflePhrases.CardinalSequence[_cardinalSequence++] );
        }

        void EvaluateOrdinalSequence( StringBuilder output )
        {
            if ( _ordinalSequence >= WafflePhrases.OrdinalSequences.Length )
            {
                _ordinalSequence = 0;
            }

            output.Append( WafflePhrases.OrdinalSequences[_ordinalSequence++] );
        }

        void RandomDate( StringBuilder output )
        {
            output.AppendFormat( "{0:04u}", DateTime.Now.Year - _random.Next( 0, 31 ) );
        }


        public static string TitleCaseWords( string input )
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase( input );
        }


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
                {
                    EvaluateRandomPhrase( WafflePhrases.MaybeHeading, result );
                }

                EvaluatePhrase( "|A |B |C |D.  ", result );
                EvaluateRandomPhrase( WafflePhrases.MaybeParagraph, result );
            }

            result.AppendLine( "</p>" );
            result.AppendLine( "</body>" );
            result.AppendLine( "</html>" );
        }

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
                {
                    EvaluateRandomPhrase( WafflePhrases.MaybeHeading, result );
                }

                EvaluatePhrase( "|A |B |C |D.  ", result );
                EvaluateRandomPhrase( WafflePhrases.MaybeParagraph, result );
            }
        }
    }
}
