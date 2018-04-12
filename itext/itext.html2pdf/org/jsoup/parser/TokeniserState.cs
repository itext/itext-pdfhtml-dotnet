using System;
using iText.IO.Util;

namespace Org.Jsoup.Parser {
    /// <summary>States and transition activations for the Tokeniser.</summary>
    internal abstract class TokeniserState {
        private sealed class _TokeniserState_10 : TokeniserState {
            public _TokeniserState_10() {
            }

            internal override String GetName() {
                return "Data";
            }

            // in data state, gather characters until a character reference or tag is found
            internal override void Read(Tokeniser t, CharacterReader r) {
                switch (r.Current()) {
                    case '&': {
                        t.AdvanceTransition(TokeniserState.CharacterReferenceInData);
                        break;
                    }

                    case '<': {
                        t.AdvanceTransition(TokeniserState.TagOpen);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        // NOT replacement character (oddly?)
                        t.Emit(r.Consume());
                        break;
                    }

                    case TokeniserState.eof: {
                        t.Emit(new Token.EOF());
                        break;
                    }

                    default: {
                        String data = r.ConsumeData();
                        t.Emit(data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState Data = new _TokeniserState_10();

        private sealed class _TokeniserState_41 : TokeniserState {
            public _TokeniserState_41() {
            }

            internal override String GetName() {
                return "CharacterReferenceInData";
            }

            // from & in data
            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadCharRef(t, TokeniserState.Data);
            }
        }

        internal static TokeniserState CharacterReferenceInData = new _TokeniserState_41();

        private sealed class _TokeniserState_54 : TokeniserState {
            public _TokeniserState_54() {
            }

            internal override String GetName() {
                return "Rcdata";
            }

            /// handles data in title, textarea etc
            internal override void Read(Tokeniser t, CharacterReader r) {
                switch (r.Current()) {
                    case '&': {
                        t.AdvanceTransition(TokeniserState.CharacterReferenceInRcdata);
                        break;
                    }

                    case '<': {
                        t.AdvanceTransition(TokeniserState.RcdataLessthanSign);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        r.Advance();
                        t.Emit(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.Emit(new Token.EOF());
                        break;
                    }

                    default: {
                        String data = r.ConsumeToAny('&', '<', TokeniserState.nullChar);
                        t.Emit(data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState Rcdata = new _TokeniserState_54();

        private sealed class _TokeniserState_86 : TokeniserState {
            public _TokeniserState_86() {
            }

            internal override String GetName() {
                return "CharacterReferenceInRcdata";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadCharRef(t, TokeniserState.Rcdata);
            }
        }

        internal static TokeniserState CharacterReferenceInRcdata = new _TokeniserState_86();

        private sealed class _TokeniserState_98 : TokeniserState {
            public _TokeniserState_98() {
            }

            internal override String GetName() {
                return "Rawtext";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadData(t, r, this, TokeniserState.RawtextLessthanSign);
            }
        }

        internal static TokeniserState Rawtext = new _TokeniserState_98();

        private sealed class _TokeniserState_110 : TokeniserState {
            public _TokeniserState_110() {
            }

            internal override String GetName() {
                return "ScriptData";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadData(t, r, this, TokeniserState.ScriptDataLessthanSign);
            }
        }

        internal static TokeniserState ScriptData = new _TokeniserState_110();

        private sealed class _TokeniserState_122 : TokeniserState {
            public _TokeniserState_122() {
            }

            internal override String GetName() {
                return "PLAINTEXT";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                switch (r.Current()) {
                    case TokeniserState.nullChar: {
                        t.Error(this);
                        r.Advance();
                        t.Emit(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.Emit(new Token.EOF());
                        break;
                    }

                    default: {
                        String data = r.ConsumeTo(TokeniserState.nullChar);
                        t.Emit(data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState PLAINTEXT = new _TokeniserState_122();

        private sealed class _TokeniserState_147 : TokeniserState {
            public _TokeniserState_147() {
            }

            internal override String GetName() {
                return "TagOpen";
            }

            // from < in data
            internal override void Read(Tokeniser t, CharacterReader r) {
                switch (r.Current()) {
                    case '!': {
                        t.AdvanceTransition(TokeniserState.MarkupDeclarationOpen);
                        break;
                    }

                    case '/': {
                        t.AdvanceTransition(TokeniserState.EndTagOpen);
                        break;
                    }

                    case '?': {
                        t.AdvanceTransition(TokeniserState.BogusComment);
                        break;
                    }

                    default: {
                        if (r.MatchesLetter()) {
                            t.CreateTagPending(true);
                            t.Transition(TokeniserState.TagName);
                        }
                        else {
                            t.Error(this);
                            t.Emit('<');
                            // char that got us here
                            t.Transition(TokeniserState.Data);
                        }
                        break;
                    }
                }
            }
        }

        internal static TokeniserState TagOpen = new _TokeniserState_147();

        private sealed class _TokeniserState_180 : TokeniserState {
            public _TokeniserState_180() {
            }

            internal override String GetName() {
                return "EndTagOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.IsEmpty()) {
                    t.EofError(this);
                    t.Emit("</");
                    t.Transition(TokeniserState.Data);
                }
                else {
                    if (r.MatchesLetter()) {
                        t.CreateTagPending(false);
                        t.Transition(TokeniserState.TagName);
                    }
                    else {
                        if (r.Matches('>')) {
                            t.Error(this);
                            t.AdvanceTransition(TokeniserState.Data);
                        }
                        else {
                            t.Error(this);
                            t.AdvanceTransition(TokeniserState.BogusComment);
                        }
                    }
                }
            }
        }

        internal static TokeniserState EndTagOpen = new _TokeniserState_180();

        private sealed class _TokeniserState_205 : TokeniserState {
            public _TokeniserState_205() {
            }

            internal override String GetName() {
                return "TagName";
            }

            // from < or </ in data, will have start or end tag pending
            internal override void Read(Tokeniser t, CharacterReader r) {
                // previous TagOpen state did NOT consume, will have a letter char in current
                //String tagName = r.consumeToAnySorted(tagCharsSorted).toLowerCase();
                String tagName = r.ConsumeTagName().ToLowerInvariant();
                t.tagPending.AppendTagName(tagName);
                switch (r.Consume()) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeAttributeName);
                        break;
                    }

                    case '/': {
                        t.Transition(TokeniserState.SelfClosingStartTag);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        // replacement
                        t.tagPending.AppendTagName(TokeniserState.replacementStr);
                        break;
                    }

                    case TokeniserState.eof: {
                        // should emit pending tag?
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState TagName = new _TokeniserState_205();

        private sealed class _TokeniserState_245 : TokeniserState {
            public _TokeniserState_245() {
            }

            // no default, as covered with above consumeToAny
            internal override String GetName() {
                return "RcdataLessthanSign";
            }

            // from < in rcdata
            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.Matches('/')) {
                    t.CreateTempBuffer();
                    t.AdvanceTransition(TokeniserState.RCDATAEndTagOpen);
                }
                else {
                    if (r.MatchesLetter() && t.AppropriateEndTagName() != null && !r.ContainsIgnoreCase("</" + t.AppropriateEndTagName
                        ())) {
                        // diverge from spec: got a start tag, but there's no appropriate end tag (</title>), so rather than
                        // consuming to EOF; break out here
                        t.tagPending = t.CreateTagPending(false).Name(t.AppropriateEndTagName());
                        t.EmitTagPending();
                        r.Unconsume();
                        // undo "<"
                        t.Transition(TokeniserState.Data);
                    }
                    else {
                        t.Emit("<");
                        t.Transition(TokeniserState.Rcdata);
                    }
                }
            }
        }

        internal static TokeniserState RcdataLessthanSign = new _TokeniserState_245();

        private sealed class _TokeniserState_271 : TokeniserState {
            public _TokeniserState_271() {
            }

            internal override String GetName() {
                return "RCDATAEndTagOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    t.CreateTagPending(false);
                    t.tagPending.AppendTagName(char.ToLower(r.Current()));
                    t.dataBuffer.Append(char.ToLower(r.Current()));
                    t.AdvanceTransition(TokeniserState.RCDATAEndTagName);
                }
                else {
                    t.Emit("</");
                    t.Transition(TokeniserState.Rcdata);
                }
            }
        }

        internal static TokeniserState RCDATAEndTagOpen = new _TokeniserState_271();

        private sealed class _TokeniserState_291 : TokeniserState {
            public _TokeniserState_291() {
            }

            internal override String GetName() {
                return "RCDATAEndTagName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    String name = r.ConsumeLetterSequence();
                    t.tagPending.AppendTagName(name.ToLowerInvariant());
                    t.dataBuffer.Append(name);
                    return;
                }
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        if (t.IsAppropriateEndTagToken()) {
                            t.Transition(TokeniserState.BeforeAttributeName);
                        }
                        else {
                            this.AnythingElse(t, r);
                        }
                        break;
                    }

                    case '/': {
                        if (t.IsAppropriateEndTagToken()) {
                            t.Transition(TokeniserState.SelfClosingStartTag);
                        }
                        else {
                            this.AnythingElse(t, r);
                        }
                        break;
                    }

                    case '>': {
                        if (t.IsAppropriateEndTagToken()) {
                            t.EmitTagPending();
                            t.Transition(TokeniserState.Data);
                        }
                        else {
                            this.AnythingElse(t, r);
                        }
                        break;
                    }

                    default: {
                        this.AnythingElse(t, r);
                        break;
                    }
                }
            }

            private void AnythingElse(Tokeniser t, CharacterReader r) {
                t.Emit("</" + t.dataBuffer.ToString());
                r.Unconsume();
                t.Transition(TokeniserState.Rcdata);
            }
        }

        internal static TokeniserState RCDATAEndTagName = new _TokeniserState_291();

        private sealed class _TokeniserState_344 : TokeniserState {
            public _TokeniserState_344() {
            }

            internal override String GetName() {
                return "RawtextLessthanSign";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.Matches('/')) {
                    t.CreateTempBuffer();
                    t.AdvanceTransition(TokeniserState.RawtextEndTagOpen);
                }
                else {
                    t.Emit('<');
                    t.Transition(TokeniserState.Rawtext);
                }
            }
        }

        internal static TokeniserState RawtextLessthanSign = new _TokeniserState_344();

        private sealed class _TokeniserState_362 : TokeniserState {
            public _TokeniserState_362() {
            }

            internal override String GetName() {
                return "RawtextEndTagOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadEndTag(t, r, TokeniserState.RawtextEndTagName, TokeniserState.Rawtext);
            }
        }

        internal static TokeniserState RawtextEndTagOpen = new _TokeniserState_362();

        private sealed class _TokeniserState_374 : TokeniserState {
            public _TokeniserState_374() {
            }

            internal override String GetName() {
                return "RawtextEndTagName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.HandleDataEndTag(t, r, TokeniserState.Rawtext);
            }
        }

        internal static TokeniserState RawtextEndTagName = new _TokeniserState_374();

        private sealed class _TokeniserState_386 : TokeniserState {
            public _TokeniserState_386() {
            }

            internal override String GetName() {
                return "ScriptDataLessthanSign";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                switch (r.Consume()) {
                    case '/': {
                        t.CreateTempBuffer();
                        t.Transition(TokeniserState.ScriptDataEndTagOpen);
                        break;
                    }

                    case '!': {
                        t.Emit("<!");
                        t.Transition(TokeniserState.ScriptDataEscapeStart);
                        break;
                    }

                    default: {
                        t.Emit("<");
                        r.Unconsume();
                        t.Transition(TokeniserState.ScriptData);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataLessthanSign = new _TokeniserState_386();

        private sealed class _TokeniserState_411 : TokeniserState {
            public _TokeniserState_411() {
            }

            internal override String GetName() {
                return "ScriptDataEndTagOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.ReadEndTag(t, r, TokeniserState.ScriptDataEndTagName, TokeniserState.ScriptData);
            }
        }

        internal static TokeniserState ScriptDataEndTagOpen = new _TokeniserState_411();

        private sealed class _TokeniserState_423 : TokeniserState {
            public _TokeniserState_423() {
            }

            internal override String GetName() {
                return "ScriptDataEndTagName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.HandleDataEndTag(t, r, TokeniserState.ScriptData);
            }
        }

        internal static TokeniserState ScriptDataEndTagName = new _TokeniserState_423();

        private sealed class _TokeniserState_435 : TokeniserState {
            public _TokeniserState_435() {
            }

            internal override String GetName() {
                return "ScriptDataEscapeStart";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.Matches('-')) {
                    t.Emit('-');
                    t.AdvanceTransition(TokeniserState.ScriptDataEscapeStartDash);
                }
                else {
                    t.Transition(TokeniserState.ScriptData);
                }
            }
        }

        internal static TokeniserState ScriptDataEscapeStart = new _TokeniserState_435();

        private sealed class _TokeniserState_452 : TokeniserState {
            public _TokeniserState_452() {
            }

            internal override String GetName() {
                return "ScriptDataEscapeStartDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.Matches('-')) {
                    t.Emit('-');
                    t.AdvanceTransition(TokeniserState.ScriptDataEscapedDashDash);
                }
                else {
                    t.Transition(TokeniserState.ScriptData);
                }
            }
        }

        internal static TokeniserState ScriptDataEscapeStartDash = new _TokeniserState_452();

        private sealed class _TokeniserState_469 : TokeniserState {
            public _TokeniserState_469() {
            }

            internal override String GetName() {
                return "ScriptDataEscaped";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.IsEmpty()) {
                    t.EofError(this);
                    t.Transition(TokeniserState.Data);
                    return;
                }
                switch (r.Current()) {
                    case '-': {
                        t.Emit('-');
                        t.AdvanceTransition(TokeniserState.ScriptDataEscapedDash);
                        break;
                    }

                    case '<': {
                        t.AdvanceTransition(TokeniserState.ScriptDataEscapedLessthanSign);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        r.Advance();
                        t.Emit(TokeniserState.replacementChar);
                        break;
                    }

                    default: {
                        String data = r.ConsumeToAny('-', '<', TokeniserState.nullChar);
                        t.Emit(data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataEscaped = new _TokeniserState_469();

        private sealed class _TokeniserState_503 : TokeniserState {
            public _TokeniserState_503() {
            }

            internal override String GetName() {
                return "ScriptDataEscapedDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.IsEmpty()) {
                    t.EofError(this);
                    t.Transition(TokeniserState.Data);
                    return;
                }
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataEscapedDashDash);
                        break;
                    }

                    case '<': {
                        t.Transition(TokeniserState.ScriptDataEscapedLessthanSign);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.Emit(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.ScriptDataEscaped);
                        break;
                    }

                    default: {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataEscaped);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataEscapedDash = new _TokeniserState_503();

        private sealed class _TokeniserState_538 : TokeniserState {
            public _TokeniserState_538() {
            }

            internal override String GetName() {
                return "ScriptDataEscapedDashDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.IsEmpty()) {
                    t.EofError(this);
                    t.Transition(TokeniserState.Data);
                    return;
                }
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Emit(c);
                        break;
                    }

                    case '<': {
                        t.Transition(TokeniserState.ScriptDataEscapedLessthanSign);
                        break;
                    }

                    case '>': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptData);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.Emit(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.ScriptDataEscaped);
                        break;
                    }

                    default: {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataEscaped);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataEscapedDashDash = new _TokeniserState_538();

        private sealed class _TokeniserState_576 : TokeniserState {
            public _TokeniserState_576() {
            }

            internal override String GetName() {
                return "ScriptDataEscapedLessthanSign";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    t.CreateTempBuffer();
                    t.dataBuffer.Append(char.ToLower(r.Current()));
                    t.Emit("<" + r.Current());
                    t.AdvanceTransition(TokeniserState.ScriptDataDoubleEscapeStart);
                }
                else {
                    if (r.Matches('/')) {
                        t.CreateTempBuffer();
                        t.AdvanceTransition(TokeniserState.ScriptDataEscapedEndTagOpen);
                    }
                    else {
                        t.Emit('<');
                        t.Transition(TokeniserState.ScriptDataEscaped);
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataEscapedLessthanSign = new _TokeniserState_576();

        private sealed class _TokeniserState_599 : TokeniserState {
            public _TokeniserState_599() {
            }

            internal override String GetName() {
                return "ScriptDataEscapedEndTagOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    t.CreateTagPending(false);
                    t.tagPending.AppendTagName(char.ToLower(r.Current()));
                    t.dataBuffer.Append(r.Current());
                    t.AdvanceTransition(TokeniserState.ScriptDataEscapedEndTagName);
                }
                else {
                    t.Emit("</");
                    t.Transition(TokeniserState.ScriptDataEscaped);
                }
            }
        }

        internal static TokeniserState ScriptDataEscapedEndTagOpen = new _TokeniserState_599();

        private sealed class _TokeniserState_619 : TokeniserState {
            public _TokeniserState_619() {
            }

            internal override String GetName() {
                return "ScriptDataEscapedEndTagName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.HandleDataEndTag(t, r, TokeniserState.ScriptDataEscaped);
            }
        }

        internal static TokeniserState ScriptDataEscapedEndTagName = new _TokeniserState_619();

        private sealed class _TokeniserState_631 : TokeniserState {
            public _TokeniserState_631() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscapeStart";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.HandleDataDoubleEscapeTag(t, r, TokeniserState.ScriptDataDoubleEscaped, TokeniserState.ScriptDataEscaped
                    );
            }
        }

        internal static TokeniserState ScriptDataDoubleEscapeStart = new _TokeniserState_631();

        private sealed class _TokeniserState_643 : TokeniserState {
            public _TokeniserState_643() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscaped";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Current();
                switch (c) {
                    case '-': {
                        t.Emit(c);
                        t.AdvanceTransition(TokeniserState.ScriptDataDoubleEscapedDash);
                        break;
                    }

                    case '<': {
                        t.Emit(c);
                        t.AdvanceTransition(TokeniserState.ScriptDataDoubleEscapedLessthanSign);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        r.Advance();
                        t.Emit(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        String data = r.ConsumeToAny('-', '<', TokeniserState.nullChar);
                        t.Emit(data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataDoubleEscaped = new _TokeniserState_643();

        private sealed class _TokeniserState_677 : TokeniserState {
            public _TokeniserState_677() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscapedDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataDoubleEscapedDashDash);
                        break;
                    }

                    case '<': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataDoubleEscapedLessthanSign);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.Emit(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.ScriptDataDoubleEscaped);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataDoubleEscaped);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataDoubleEscapedDash = new _TokeniserState_677();

        private sealed class _TokeniserState_711 : TokeniserState {
            public _TokeniserState_711() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscapedDashDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Emit(c);
                        break;
                    }

                    case '<': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataDoubleEscapedLessthanSign);
                        break;
                    }

                    case '>': {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptData);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.Emit(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.ScriptDataDoubleEscaped);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Emit(c);
                        t.Transition(TokeniserState.ScriptDataDoubleEscaped);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState ScriptDataDoubleEscapedDashDash = new _TokeniserState_711();

        private sealed class _TokeniserState_748 : TokeniserState {
            public _TokeniserState_748() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscapedLessthanSign";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.Matches('/')) {
                    t.Emit('/');
                    t.CreateTempBuffer();
                    t.AdvanceTransition(TokeniserState.ScriptDataDoubleEscapeEnd);
                }
                else {
                    t.Transition(TokeniserState.ScriptDataDoubleEscaped);
                }
            }
        }

        internal static TokeniserState ScriptDataDoubleEscapedLessthanSign = new _TokeniserState_748();

        private sealed class _TokeniserState_766 : TokeniserState {
            public _TokeniserState_766() {
            }

            internal override String GetName() {
                return "ScriptDataDoubleEscapeEnd";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                TokeniserState.HandleDataDoubleEscapeTag(t, r, TokeniserState.ScriptDataEscaped, TokeniserState.ScriptDataDoubleEscaped
                    );
            }
        }

        internal static TokeniserState ScriptDataDoubleEscapeEnd = new _TokeniserState_766();

        private sealed class _TokeniserState_778 : TokeniserState {
            public _TokeniserState_778() {
            }

            internal override String GetName() {
                return "BeforeAttributeName";
            }

            // from tagname <xxx
            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case '/': {
                        // ignore whitespace
                        t.Transition(TokeniserState.SelfClosingStartTag);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.NewAttribute();
                        r.Unconsume();
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"':
                    case '\'':
                    case '<':
                    case '=': {
                        t.Error(this);
                        t.tagPending.NewAttribute();
                        t.tagPending.AppendAttributeName(c);
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }

                    default: {
                        // A-Z, anything else
                        t.tagPending.NewAttribute();
                        r.Unconsume();
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BeforeAttributeName = new _TokeniserState_778();

        private sealed class _TokeniserState_829 : TokeniserState {
            public _TokeniserState_829() {
            }

            internal override String GetName() {
                return "AttributeName";
            }

            // from before attribute name
            internal override void Read(Tokeniser t, CharacterReader r) {
                String name = r.ConsumeToAnySorted(TokeniserState.attributeNameCharsSorted);
                t.tagPending.AppendAttributeName(name.ToLowerInvariant());
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.AfterAttributeName);
                        break;
                    }

                    case '/': {
                        t.Transition(TokeniserState.SelfClosingStartTag);
                        break;
                    }

                    case '=': {
                        t.Transition(TokeniserState.BeforeAttributeValue);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeName(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"':
                    case '\'':
                    case '<': {
                        t.Error(this);
                        t.tagPending.AppendAttributeName(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AttributeName = new _TokeniserState_829();

        private sealed class _TokeniserState_878 : TokeniserState {
            public _TokeniserState_878() {
            }

            // no default, as covered in consumeToAny
            internal override String GetName() {
                return "AfterAttributeName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        // ignore
                        break;
                    }

                    case '/': {
                        t.Transition(TokeniserState.SelfClosingStartTag);
                        break;
                    }

                    case '=': {
                        t.Transition(TokeniserState.BeforeAttributeValue);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeName(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"':
                    case '\'':
                    case '<': {
                        t.Error(this);
                        t.tagPending.NewAttribute();
                        t.tagPending.AppendAttributeName(c);
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }

                    default: {
                        // A-Z, anything else
                        t.tagPending.NewAttribute();
                        r.Unconsume();
                        t.Transition(TokeniserState.AttributeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterAttributeName = new _TokeniserState_878();

        private sealed class _TokeniserState_930 : TokeniserState {
            public _TokeniserState_930() {
            }

            internal override String GetName() {
                return "BeforeAttributeValue";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        // ignore
                        break;
                    }

                    case '"': {
                        t.Transition(TokeniserState.AttributeValue_doubleQuoted);
                        break;
                    }

                    case '&': {
                        r.Unconsume();
                        t.Transition(TokeniserState.AttributeValue_unquoted);
                        break;
                    }

                    case '\'': {
                        t.Transition(TokeniserState.AttributeValue_singleQuoted);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.AttributeValue_unquoted);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '<':
                    case '=':
                    case '`': {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(c);
                        t.Transition(TokeniserState.AttributeValue_unquoted);
                        break;
                    }

                    default: {
                        r.Unconsume();
                        t.Transition(TokeniserState.AttributeValue_unquoted);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BeforeAttributeValue = new _TokeniserState_930();

        private sealed class _TokeniserState_986 : TokeniserState {
            public _TokeniserState_986() {
            }

            internal override String GetName() {
                return "AttributeValue_doubleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                String value = r.ConsumeToAny(TokeniserState.attributeDoubleValueCharsSorted);
                if (value.Length > 0) {
                    t.tagPending.AppendAttributeValue(value);
                }
                else {
                    t.tagPending.SetEmptyAttributeValue();
                }
                char c = r.Consume();
                switch (c) {
                    case '"': {
                        t.Transition(TokeniserState.AfterAttributeValue_quoted);
                        break;
                    }

                    case '&': {
                        char[] @ref = t.ConsumeCharacterReference('"', true);
                        if (@ref != null) {
                            t.tagPending.AppendAttributeValue(@ref);
                        }
                        else {
                            t.tagPending.AppendAttributeValue('&');
                        }
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AttributeValue_doubleQuoted = new _TokeniserState_986();

        private sealed class _TokeniserState_1025 : TokeniserState {
            public _TokeniserState_1025() {
            }

            // no default, handled in consume to any above
            internal override String GetName() {
                return "AttributeValue_singleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                String value = r.ConsumeToAny(TokeniserState.attributeSingleValueCharsSorted);
                if (value.Length > 0) {
                    t.tagPending.AppendAttributeValue(value);
                }
                else {
                    t.tagPending.SetEmptyAttributeValue();
                }
                char c = r.Consume();
                switch (c) {
                    case '\'': {
                        t.Transition(TokeniserState.AfterAttributeValue_quoted);
                        break;
                    }

                    case '&': {
                        char[] @ref = t.ConsumeCharacterReference('\'', true);
                        if (@ref != null) {
                            t.tagPending.AppendAttributeValue(@ref);
                        }
                        else {
                            t.tagPending.AppendAttributeValue('&');
                        }
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AttributeValue_singleQuoted = new _TokeniserState_1025();

        private sealed class _TokeniserState_1064 : TokeniserState {
            public _TokeniserState_1064() {
            }

            // no default, handled in consume to any above
            internal override String GetName() {
                return "AttributeValue_unquoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                String value = r.ConsumeToAnySorted(TokeniserState.attributeValueUnquoted);
                if (value.Length > 0) {
                    t.tagPending.AppendAttributeValue(value);
                }
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeAttributeName);
                        break;
                    }

                    case '&': {
                        char[] @ref = t.ConsumeCharacterReference('>', true);
                        if (@ref != null) {
                            t.tagPending.AppendAttributeValue(@ref);
                        }
                        else {
                            t.tagPending.AppendAttributeValue('&');
                        }
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"':
                    case '\'':
                    case '<':
                    case '=':
                    case '`': {
                        t.Error(this);
                        t.tagPending.AppendAttributeValue(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AttributeValue_unquoted = new _TokeniserState_1064();

        private sealed class _TokeniserState_1119 : TokeniserState {
            public _TokeniserState_1119() {
            }

            // no default, handled in consume to any above
            // CharacterReferenceInAttributeValue state handled inline
            internal override String GetName() {
                return "AfterAttributeValue_quoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeAttributeName);
                        break;
                    }

                    case '/': {
                        t.Transition(TokeniserState.SelfClosingStartTag);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        r.Unconsume();
                        t.Transition(TokeniserState.BeforeAttributeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterAttributeValue_quoted = new _TokeniserState_1119();

        private sealed class _TokeniserState_1156 : TokeniserState {
            public _TokeniserState_1156() {
            }

            internal override String GetName() {
                return "SelfClosingStartTag";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '>': {
                        t.tagPending.selfClosing = true;
                        t.EmitTagPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.Transition(TokeniserState.BeforeAttributeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState SelfClosingStartTag = new _TokeniserState_1156();

        private sealed class _TokeniserState_1182 : TokeniserState {
            public _TokeniserState_1182() {
            }

            internal override String GetName() {
                return "BogusComment";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                // todo: handle bogus comment starting from eof. when does that trigger?
                // rewind to capture character that lead us here
                r.Unconsume();
                Token.Comment comment = new Token.Comment();
                comment.bogus = true;
                comment.data.Append(r.ConsumeTo('>'));
                // todo: replace nullChar with replaceChar
                t.Emit(comment);
                t.AdvanceTransition(TokeniserState.Data);
            }
        }

        internal static TokeniserState BogusComment = new _TokeniserState_1182();

        private sealed class _TokeniserState_1202 : TokeniserState {
            public _TokeniserState_1202() {
            }

            internal override String GetName() {
                return "MarkupDeclarationOpen";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchConsume("--")) {
                    t.CreateCommentPending();
                    t.Transition(TokeniserState.CommentStart);
                }
                else {
                    if (r.MatchConsumeIgnoreCase("DOCTYPE")) {
                        t.Transition(TokeniserState.Doctype);
                    }
                    else {
                        if (r.MatchConsume("[CDATA[")) {
                            // todo: should actually check current namepspace, and only non-html allows cdata. until namespace
                            // is implemented properly, keep handling as cdata
                            //} else if (!t.currentNodeInHtmlNS() && r.matchConsume("[CDATA[")) {
                            t.Transition(TokeniserState.CdataSection);
                        }
                        else {
                            t.Error(this);
                            t.AdvanceTransition(TokeniserState.BogusComment);
                        }
                    }
                }
            }
        }

        internal static TokeniserState MarkupDeclarationOpen = new _TokeniserState_1202();

        private sealed class _TokeniserState_1227 : TokeniserState {
            public _TokeniserState_1227() {
            }

            // advance so this character gets in bogus comment data's rewind
            internal override String GetName() {
                return "CommentStart";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Transition(TokeniserState.CommentStartDash);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.commentPending.data.Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.commentPending.data.Append(c);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState CommentStart = new _TokeniserState_1227();

        private sealed class _TokeniserState_1262 : TokeniserState {
            public _TokeniserState_1262() {
            }

            internal override String GetName() {
                return "CommentStartDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Transition(TokeniserState.CommentStartDash);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.commentPending.data.Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.commentPending.data.Append(c);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState CommentStartDash = new _TokeniserState_1262();

        private sealed class _TokeniserState_1297 : TokeniserState {
            public _TokeniserState_1297() {
            }

            internal override String GetName() {
                return "Comment";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Current();
                switch (c) {
                    case '-': {
                        t.AdvanceTransition(TokeniserState.CommentEndDash);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        r.Advance();
                        t.commentPending.data.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.commentPending.data.Append(r.ConsumeToAny('-', TokeniserState.nullChar));
                        break;
                    }
                }
            }
        }

        internal static TokeniserState Comment = new _TokeniserState_1297();

        private sealed class _TokeniserState_1326 : TokeniserState {
            public _TokeniserState_1326() {
            }

            internal override String GetName() {
                return "CommentEndDash";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.Transition(TokeniserState.CommentEnd);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.commentPending.data.Append('-').Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.commentPending.data.Append('-').Append(c);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState CommentEndDash = new _TokeniserState_1326();

        private sealed class _TokeniserState_1356 : TokeniserState {
            public _TokeniserState_1356() {
            }

            internal override String GetName() {
                return "CommentEnd";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '>': {
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.commentPending.data.Append("--").Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }

                    case '!': {
                        t.Error(this);
                        t.Transition(TokeniserState.CommentEndBang);
                        break;
                    }

                    case '-': {
                        t.Error(this);
                        t.commentPending.data.Append('-');
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.commentPending.data.Append("--").Append(c);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState CommentEnd = new _TokeniserState_1356();

        private sealed class _TokeniserState_1396 : TokeniserState {
            public _TokeniserState_1396() {
            }

            internal override String GetName() {
                return "CommentEndBang";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '-': {
                        t.commentPending.data.Append("--!");
                        t.Transition(TokeniserState.CommentEndDash);
                        break;
                    }

                    case '>': {
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.commentPending.data.Append("--!").Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.EmitCommentPending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.commentPending.data.Append("--!").Append(c);
                        t.Transition(TokeniserState.Comment);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState CommentEndBang = new _TokeniserState_1396();

        private sealed class _TokeniserState_1431 : TokeniserState {
            public _TokeniserState_1431() {
            }

            internal override String GetName() {
                return "Doctype";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeDoctypeName);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        goto case '>';
                    }

                    case '>': {
                        // note: fall through to > case
                        // catch invalid <!DOCTYPE>
                        t.Error(this);
                        t.CreateDoctypePending();
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.Transition(TokeniserState.BeforeDoctypeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState Doctype = new _TokeniserState_1431();

        private sealed class _TokeniserState_1465 : TokeniserState {
            public _TokeniserState_1465() {
            }

            internal override String GetName() {
                return "BeforeDoctypeName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    t.CreateDoctypePending();
                    t.Transition(TokeniserState.DoctypeName);
                    return;
                }
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case TokeniserState.nullChar: {
                        // ignore whitespace
                        t.Error(this);
                        t.CreateDoctypePending();
                        t.doctypePending.name.Append(TokeniserState.replacementChar);
                        t.Transition(TokeniserState.DoctypeName);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.CreateDoctypePending();
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.CreateDoctypePending();
                        t.doctypePending.name.Append(c);
                        t.Transition(TokeniserState.DoctypeName);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BeforeDoctypeName = new _TokeniserState_1465();

        private sealed class _TokeniserState_1507 : TokeniserState {
            public _TokeniserState_1507() {
            }

            internal override String GetName() {
                return "DoctypeName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.MatchesLetter()) {
                    String name = r.ConsumeLetterSequence();
                    t.doctypePending.name.Append(name.ToLowerInvariant());
                    return;
                }
                char c = r.Consume();
                switch (c) {
                    case '>': {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.AfterDoctypeName);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.doctypePending.name.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.doctypePending.name.Append(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState DoctypeName = new _TokeniserState_1507();

        private sealed class _TokeniserState_1549 : TokeniserState {
            public _TokeniserState_1549() {
            }

            internal override String GetName() {
                return "AfterDoctypeName";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                if (r.IsEmpty()) {
                    t.EofError(this);
                    t.doctypePending.forceQuirks = true;
                    t.EmitDoctypePending();
                    t.Transition(TokeniserState.Data);
                    return;
                }
                if (r.MatchesAny('\t', '\n', '\r', '\f', ' ')) {
                    r.Advance();
                }
                else {
                    // ignore whitespace
                    if (r.Matches('>')) {
                        t.EmitDoctypePending();
                        t.AdvanceTransition(TokeniserState.Data);
                    }
                    else {
                        if (r.MatchConsumeIgnoreCase("PUBLIC")) {
                            t.Transition(TokeniserState.AfterDoctypePublicKeyword);
                        }
                        else {
                            if (r.MatchConsumeIgnoreCase("SYSTEM")) {
                                t.Transition(TokeniserState.AfterDoctypeSystemKeyword);
                            }
                            else {
                                t.Error(this);
                                t.doctypePending.forceQuirks = true;
                                t.AdvanceTransition(TokeniserState.BogusDoctype);
                            }
                        }
                    }
                }
            }
        }

        internal static TokeniserState AfterDoctypeName = new _TokeniserState_1549();

        private sealed class _TokeniserState_1582 : TokeniserState {
            public _TokeniserState_1582() {
            }

            internal override String GetName() {
                return "AfterDoctypePublicKeyword";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeDoctypePublicIdentifier);
                        break;
                    }

                    case '"': {
                        t.Error(this);
                        // set public id to empty string
                        t.Transition(TokeniserState.DoctypePublicIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        t.Error(this);
                        // set public id to empty string
                        t.Transition(TokeniserState.DoctypePublicIdentifier_singleQuoted);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterDoctypePublicKeyword = new _TokeniserState_1582();

        private sealed class _TokeniserState_1629 : TokeniserState {
            public _TokeniserState_1629() {
            }

            internal override String GetName() {
                return "BeforeDoctypePublicIdentifier";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case '"': {
                        // set public id to empty string
                        t.Transition(TokeniserState.DoctypePublicIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        // set public id to empty string
                        t.Transition(TokeniserState.DoctypePublicIdentifier_singleQuoted);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BeforeDoctypePublicIdentifier = new _TokeniserState_1629();

        private sealed class _TokeniserState_1673 : TokeniserState {
            public _TokeniserState_1673() {
            }

            internal override String GetName() {
                return "DoctypePublicIdentifier_doubleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '"': {
                        t.Transition(TokeniserState.AfterDoctypePublicIdentifier);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.doctypePending.publicIdentifier.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.doctypePending.publicIdentifier.Append(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState DoctypePublicIdentifier_doubleQuoted = new _TokeniserState_1673();

        private sealed class _TokeniserState_1708 : TokeniserState {
            public _TokeniserState_1708() {
            }

            internal override String GetName() {
                return "DoctypePublicIdentifier_singleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\'': {
                        t.Transition(TokeniserState.AfterDoctypePublicIdentifier);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.doctypePending.publicIdentifier.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.doctypePending.publicIdentifier.Append(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState DoctypePublicIdentifier_singleQuoted = new _TokeniserState_1708();

        private sealed class _TokeniserState_1743 : TokeniserState {
            public _TokeniserState_1743() {
            }

            internal override String GetName() {
                return "AfterDoctypePublicIdentifier";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BetweenDoctypePublicAndSystemIdentifiers);
                        break;
                    }

                    case '>': {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_singleQuoted);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterDoctypePublicIdentifier = new _TokeniserState_1743();

        private sealed class _TokeniserState_1788 : TokeniserState {
            public _TokeniserState_1788() {
            }

            internal override String GetName() {
                return "BetweenDoctypePublicAndSystemIdentifiers";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case '>': {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_singleQuoted);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BetweenDoctypePublicAndSystemIdentifiers = new _TokeniserState_1788();

        private sealed class _TokeniserState_1832 : TokeniserState {
            public _TokeniserState_1832() {
            }

            internal override String GetName() {
                return "AfterDoctypeSystemKeyword";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(TokeniserState.BeforeDoctypeSystemIdentifier);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case '"': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        t.Error(this);
                        // system id empty
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_singleQuoted);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterDoctypeSystemKeyword = new _TokeniserState_1832();

        private sealed class _TokeniserState_1879 : TokeniserState {
            public _TokeniserState_1879() {
            }

            internal override String GetName() {
                return "BeforeDoctypeSystemIdentifier";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case '"': {
                        // set system id to empty string
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_doubleQuoted);
                        break;
                    }

                    case '\'': {
                        // set public id to empty string
                        t.Transition(TokeniserState.DoctypeSystemIdentifier_singleQuoted);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BeforeDoctypeSystemIdentifier = new _TokeniserState_1879();

        private sealed class _TokeniserState_1923 : TokeniserState {
            public _TokeniserState_1923() {
            }

            internal override String GetName() {
                return "DoctypeSystemIdentifier_doubleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '"': {
                        t.Transition(TokeniserState.AfterDoctypeSystemIdentifier);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.doctypePending.systemIdentifier.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.doctypePending.systemIdentifier.Append(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState DoctypeSystemIdentifier_doubleQuoted = new _TokeniserState_1923();

        private sealed class _TokeniserState_1958 : TokeniserState {
            public _TokeniserState_1958() {
            }

            internal override String GetName() {
                return "DoctypeSystemIdentifier_singleQuoted";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\'': {
                        t.Transition(TokeniserState.AfterDoctypeSystemIdentifier);
                        break;
                    }

                    case TokeniserState.nullChar: {
                        t.Error(this);
                        t.doctypePending.systemIdentifier.Append(TokeniserState.replacementChar);
                        break;
                    }

                    case '>': {
                        t.Error(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.doctypePending.systemIdentifier.Append(c);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState DoctypeSystemIdentifier_singleQuoted = new _TokeniserState_1958();

        private sealed class _TokeniserState_1993 : TokeniserState {
            public _TokeniserState_1993() {
            }

            internal override String GetName() {
                return "AfterDoctypeSystemIdentifier";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        break;
                    }

                    case '>': {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EofError(this);
                        t.doctypePending.forceQuirks = true;
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        t.Error(this);
                        t.Transition(TokeniserState.BogusDoctype);
                        break;
                    }
                }
            }
        }

        internal static TokeniserState AfterDoctypeSystemIdentifier = new _TokeniserState_1993();

        private sealed class _TokeniserState_2027 : TokeniserState {
            public _TokeniserState_2027() {
            }

            // NOT force quirks
            internal override String GetName() {
                return "BogusDoctype";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                char c = r.Consume();
                switch (c) {
                    case '>': {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    case TokeniserState.eof: {
                        t.EmitDoctypePending();
                        t.Transition(TokeniserState.Data);
                        break;
                    }

                    default: {
                        // ignore char
                        break;
                    }
                }
            }
        }

        internal static TokeniserState BogusDoctype = new _TokeniserState_2027();

        private sealed class _TokeniserState_2052 : TokeniserState {
            public _TokeniserState_2052() {
            }

            internal override String GetName() {
                return "CdataSection";
            }

            internal override void Read(Tokeniser t, CharacterReader r) {
                String data = r.ConsumeTo("]]>");
                t.Emit(data);
                r.MatchConsume("]]>");
                t.Transition(TokeniserState.Data);
            }
        }

        internal static TokeniserState CdataSection = new _TokeniserState_2052();

        public override String ToString() {
            return GetName();
        }

        internal abstract String GetName();

        internal abstract void Read(Tokeniser t, CharacterReader r);

        internal const char nullChar = '\u0000';

        private static readonly char[] attributeSingleValueCharsSorted = new char[] { '\'', '&', nullChar };

        private static readonly char[] attributeDoubleValueCharsSorted = new char[] { '"', '&', nullChar };

        private static readonly char[] attributeNameCharsSorted = new char[] { '\t', '\n', '\r', '\f', ' ', '/', '='
            , '>', nullChar, '"', '\'', '<' };

        private static readonly char[] attributeValueUnquoted = new char[] { '\t', '\n', '\r', '\f', ' ', '&', '>'
            , nullChar, '"', '\'', '<', '=', '`' };

        private const char replacementChar = Tokeniser.replacementChar;

        private static readonly String replacementStr = Tokeniser.replacementChar.ToString();

        private const char eof = CharacterReader.EOF;

        static TokeniserState() {
            JavaUtil.Sort(attributeSingleValueCharsSorted);
            JavaUtil.Sort(attributeDoubleValueCharsSorted);
            JavaUtil.Sort(attributeNameCharsSorted);
            JavaUtil.Sort(attributeValueUnquoted);
        }

        /// <summary>Handles RawtextEndTagName, ScriptDataEndTagName, and ScriptDataEscapedEndTagName.</summary>
        /// <remarks>
        /// Handles RawtextEndTagName, ScriptDataEndTagName, and ScriptDataEscapedEndTagName. Same body impl, just
        /// different else exit transitions.
        /// </remarks>
        private static void HandleDataEndTag(Tokeniser t, CharacterReader r, TokeniserState elseTransition) {
            if (r.MatchesLetter()) {
                String name = r.ConsumeLetterSequence();
                t.tagPending.AppendTagName(name.ToLowerInvariant());
                t.dataBuffer.Append(name);
                return;
            }
            bool needsExitTransition = false;
            if (t.IsAppropriateEndTagToken() && !r.IsEmpty()) {
                char c = r.Consume();
                switch (c) {
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\f':
                    case ' ': {
                        t.Transition(BeforeAttributeName);
                        break;
                    }

                    case '/': {
                        t.Transition(SelfClosingStartTag);
                        break;
                    }

                    case '>': {
                        t.EmitTagPending();
                        t.Transition(Data);
                        break;
                    }

                    default: {
                        t.dataBuffer.Append(c);
                        needsExitTransition = true;
                        break;
                    }
                }
            }
            else {
                needsExitTransition = true;
            }
            if (needsExitTransition) {
                t.Emit("</" + t.dataBuffer.ToString());
                t.Transition(elseTransition);
            }
        }

        private static void ReadData(Tokeniser t, CharacterReader r, TokeniserState current, TokeniserState advance
            ) {
            switch (r.Current()) {
                case '<': {
                    t.AdvanceTransition(advance);
                    break;
                }

                case nullChar: {
                    t.Error(current);
                    r.Advance();
                    t.Emit(replacementChar);
                    break;
                }

                case eof: {
                    t.Emit(new Token.EOF());
                    break;
                }

                default: {
                    String data = r.ConsumeToAny('<', nullChar);
                    t.Emit(data);
                    break;
                }
            }
        }

        private static void ReadCharRef(Tokeniser t, TokeniserState advance) {
            char[] c = t.ConsumeCharacterReference(null, false);
            if (c == null) {
                t.Emit('&');
            }
            else {
                t.Emit(c);
            }
            t.Transition(advance);
        }

        private static void ReadEndTag(Tokeniser t, CharacterReader r, TokeniserState a, TokeniserState b) {
            if (r.MatchesLetter()) {
                t.CreateTagPending(false);
                t.Transition(a);
            }
            else {
                t.Emit("</");
                t.Transition(b);
            }
        }

        private static void HandleDataDoubleEscapeTag(Tokeniser t, CharacterReader r, TokeniserState primary, TokeniserState
             fallback) {
            if (r.MatchesLetter()) {
                String name = r.ConsumeLetterSequence();
                t.dataBuffer.Append(name.ToLowerInvariant());
                t.Emit(name);
                return;
            }
            char c = r.Consume();
            switch (c) {
                case '\t':
                case '\n':
                case '\r':
                case '\f':
                case ' ':
                case '/':
                case '>': {
                    if (t.dataBuffer.ToString().Equals("script")) {
                        t.Transition(primary);
                    }
                    else {
                        t.Transition(fallback);
                    }
                    t.Emit(c);
                    break;
                }

                default: {
                    r.Unconsume();
                    t.Transition(fallback);
                    break;
                }
            }
        }
    }
}
