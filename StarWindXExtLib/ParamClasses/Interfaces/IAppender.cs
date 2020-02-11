using StarWindXLib;

namespace StarWindXExtLib
{

    public interface IAppender
    {

        void AppendParams(IParameters pars);

        void AppendParam(string paramName, string paramValue);

        Parameters GenerateParams();

        T GenerateParams<T>() where T : IParameters, new();
    }
}