using System;
namespace Swallow.Model
{
	public class MayFail<R>
	{
		readonly bool isError;
		readonly R result;
		readonly string errorMessage;
		
		internal MayFail(bool pIsError, R pResult, string pErrorMessage)
		{
			this.isError = pIsError;
			this.result = pResult;
			this.errorMessage = pErrorMessage;
		}

		public bool IsError
		{
			get { return this.isError; }
		}

		public R Result
		{
			get
			{
				if (this.isError) throw new InvalidOperationException("このオブジェクトはエラー状態であるため結果を取り出すことは出来ません.");
				return this.result;
			}
		}

		public string ErrorMessage
		{
			get
			{
				if (!this.isError) throw new InvalidOperationException("このオブジェクトはエラー状態ではないためエラーメッセージを取り出すことは出来ません.");
				return this.errorMessage;
			}
		}

		// このメソッドをMayFailクラスの中に書いてしまうと、呼び出し側が非常に不恰好になってしまう.
		// var res = MayFail<なんでもいいから型名>.NewSuccess<本当に扱いたい型>(result);
		// これは耐えられない！！
		//public static MayFail<T> NewSuccess<T>(T pResult)
		//{
		//	return null;
		//}
	}

	public static class MayFailFactory
	{
		public static MayFail<T> NewSuccess<T>(T pResult)
		{
			return new MayFail<T>(false, pResult, null);
		}

		public static MayFail<T> NewFail<T>(string pErrorMessage)
		{
			return new MayFail<T>(true, default(T), pErrorMessage);
		}
	}
}
