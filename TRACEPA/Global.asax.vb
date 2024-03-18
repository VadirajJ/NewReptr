Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        HttpContext.Current.ClearError()
        Response.Redirect("~/ServerError.aspx", False)
    End Sub
End Class