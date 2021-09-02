using System;

namespace MediatorBoilerplate.Domain.Core.Base.Entities
{
    //////////////////////////////////////////////////
    //                                              //
    //              .---.                           //
    //             /o   o\                          //
    //          __(=  "  =)__                       //
    //           //\'-=-'/\\                        //
    //              )   (_                          //
    //             /      `"=-._                    //
    //            /       \     ``"=.               //
    //           /  /   \  \         `=..--.        //
    //       ___/  /     \  \___      _,  , `\      //
    //      `-----' `""""`'-----``"""`  \  \_/      //
    //                                   `-`        //
    //                                              //
    //////////////////////////////////////////////////
    public interface IIdentifier<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IAuditEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}