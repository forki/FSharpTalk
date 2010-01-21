using System.Collections.Generic;

namespace CSharp
{
    public class EMail
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public enum SpamResult
    {
        Spam,
        Ok,
        Unknown
    }

    public class RuleChecker1
    {
        public SpamResult CheckMail(EMail mail)
        {
            var result = TestRule1(mail);

            if (result != SpamResult.Unknown)
                return result;

            result = TestRule2(mail);
            if (result != SpamResult.Unknown)
                return result;

            // …
            return SpamResult.Unknown;
        }

        private static SpamResult TestRule1(EMail mail)
        {
            // I don’t care about the concrete rules
            return SpamResult.Unknown;
        }

        private static SpamResult TestRule2(EMail mail)
        {
            // I don’t care about the concrete rules
            return SpamResult.Ok;
        }
    }


    //  Open/Closed Principle - The O in SOLID 
    // "Class should be open for extensions but closed for modifications."

    public interface ISpamRule
    {
        SpamResult CheckMail(EMail mail);
    }

    public class RuleChecker
    {
        private readonly IEnumerable<ISpamRule> _rules;

        public RuleChecker(IEnumerable<ISpamRule> rules)
        {
            _rules = rules;
        }

        public SpamResult CheckMail(EMail mail)
        {
            foreach (var rule in _rules)
            {
                var result = rule.CheckMail(mail);

                if (result != SpamResult.Unknown)
                    return result;
            }

            return SpamResult.Unknown;
        }
    }

    class MyFirstRule : ISpamRule
    {
        public SpamResult CheckMail(EMail mail)
        {
            // I don’t care about this
            return SpamResult.Unknown;
        }
    }


    class MySecondRule : ISpamRule
    {
        public SpamResult CheckMail(EMail mail)
        {
            // I don’t care about this
            return SpamResult.Ok;
        }
    }
    
    // …    

    public class MailApp
    {
        public void Check(EMail mail)
        {
            // Bootstrapping
            var ruleChecker =
                new RuleChecker(
                    new List<ISpamRule>
                {
                    new MyFirstRule(),
                    new MySecondRule(),
                    // …
                });

            ruleChecker.CheckMail(mail);
            
            // ...
        }
    }

}
