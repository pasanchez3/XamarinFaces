package md59b54c37d997c6329acfcc08fd8a1784e;


public class MainActivity_IdentificationTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_onProgressUpdate:([Ljava/lang/Object;)V:GetOnProgressUpdate_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("XamarinFaces.MainActivity+IdentificationTask, XamarinFaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_IdentificationTask.class, __md_methods);
	}


	public MainActivity_IdentificationTask () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_IdentificationTask.class)
			mono.android.TypeManager.Activate ("XamarinFaces.MainActivity+IdentificationTask, XamarinFaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_IdentificationTask (md59b54c37d997c6329acfcc08fd8a1784e.MainActivity p0, java.lang.String p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_IdentificationTask.class)
			mono.android.TypeManager.Activate ("XamarinFaces.MainActivity+IdentificationTask, XamarinFaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "XamarinFaces.MainActivity, XamarinFaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public void onProgressUpdate (java.lang.Object[] p0)
	{
		n_onProgressUpdate (p0);
	}

	private native void n_onProgressUpdate (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
