Public Class Form1

    'DECLARE FUNCTION F3# (x AS DOUBLE)
    Structure vector
        Dim x As Double
        Dim y As Double
        Dim xn As Double
        Dim yn As Double
    End Structure
    Structure Ivector
        Dim x As Integer
        Dim y As Integer
        Dim xn As Integer
        Dim yn As Integer
    End Structure

    Structure wndow
        Dim xmin As Double
        Dim ymin As Double
        Dim xmax As Double
        Dim ymax As Double
    End Structure
    Public v1 As vector, v2 As vector, v3 As vector

    Public window As wndow
    Public w As wndow
    Public scnres As vector
    Public stp As vector
    Public nstp As vector
    Public ln As vector
    'public window.xmin AS DOUBLE, window.xmax  AS DOUBLE, window.ymin AS DOUBLE, window.ymax AS DOUBLE
    Public xmin As Double, xmax As Double, ymin As Double, ymax As Double
    'SHARED w.xmin AS DOUBLE, w.xmax AS DOUBLE, w.ymin AS DOUBLE, w.ymax AS DOUBLE
    'SHARED scnres.x AS INTEGER, scnres.y AS INTEGER
    Public PI As Double, one As Double
    'SHARED stp.x AS DOUBLE, stp.y AS DOUBLE
    'SHARED nstp.x AS DOUBLE, nstp.y AS DOUBLE
    Dim I As Double
    Public dmode As String
    Public zoom As Double
    Public drwaxis As Integer
    Public drwrul As Integer
    Public drwmode As String
    Public trace As Integer, Xtrace As Double
    Public df1 As Integer, df2 As Integer, df3 As Integer
    Public MouseP As vector
    Public MouseV As vector
    Public MouseV2 As vector
    Public MouseT(2) As Double
    Public MPoint As System.Drawing.Point
    Public MouseP2 As vector
    Public clock As New Microsoft.VisualBasic.Devices.Clock
    Public BallMoving As Boolean
    Public BPos As vector
    Public BVol As vector
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Dim xcenter As Double, ycenter As Double
        xcenter = window.xmin + dis(window.xmin, 0, window.xmax, 0) / 2
        ycenter = window.ymin + dis(0, window.ymin, 0, window.ymax) / 2

        xmin = xcenter - (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        xmax = xcenter + (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        ymin = ycenter - (dis(0, window.ymin, 0, window.ymax) / 2) * zoom
        ymax = ycenter + (dis(0, window.ymin, 0, window.ymax) / 2) * zoom



        Dim s As Integer
         Dim x As Double, y As Double
        Dim cl As Integer
        cl = 0
        s = e.KeyCode
      

        Select Case s
            Case Keys.M
                MouseT(2) = MouseT(1)
                MouseT(1) = clock.TickCount
                Call render()
            Case Keys.Escape
                'font.Dispose()
                End
            Case Keys.Up
                'Me.PictureBox1.Refresh()
                'PRINT "up"

                window.ymax = window.ymax + stp.y * zoom
                window.ymin = window.ymin + stp.y * zoom
                Call render()
            Case Keys.Left
                Me.PictureBox1.Refresh()
                'PRINT "left"
                window.xmin = window.xmin - stp.x * zoom
                window.xmax = window.xmax - stp.x * zoom
                Call render()
            Case Keys.Down
                Me.PictureBox1.Refresh()
                'PRINT "down"
                window.ymin = window.ymin - stp.y * zoom
                window.ymax = window.ymax - stp.y * zoom
                Call render()
            Case Keys.Right
                Me.PictureBox1.Refresh()
                'PRINT "right"
                window.xmin = window.xmin + stp.x * zoom
                window.xmax = window.xmax + stp.x * zoom
                Call render()

            Case Keys.A
                x = v1.x * Math.Cos(PI / 12) - Math.Sin(PI / 12) * v1.y
                y = v1.x * Math.Sin(PI / 12) + Math.Cos(PI / 12) * v1.y
                v1.x = x
                v1.y = y
                Call render()
            Case Keys.Z
                x = v1.x * Math.Cos(-(PI / 12)) - Math.Sin(-(PI / 12)) * v1.y
                y = v1.x * Math.Sin(-(PI / 12)) + Math.Cos(-(PI / 12)) * v1.y
                v1.x = x
                v1.y = y
                Call render()
            Case Keys.S
                x = v2.x * Math.Cos(PI / 12) - Math.Sin(PI / 12) * v2.y
                y = v2.x * Math.Sin(PI / 12) + Math.Cos(PI / 12) * v2.y
                v2.x = x
                v2.y = y
                Call render()
            Case Keys.X
                x = v2.x * Math.Cos(-(PI / 12)) - Math.Sin(-(PI / 12)) * v2.y
                y = v2.x * Math.Sin(-(PI / 12)) + Math.Cos(-(PI / 12)) * v2.y
                v2.x = x
                v2.y = y
                Call render()
            Case Keys.NumPad1
                dmode = "function"
                Call render()
            Case Keys.NumPad2
                dmode = "vector"
                Call render()
            Case Keys.NumPad3
                dmode = "polar"
                Call render()
            Case Keys.OemMinus
                zoom = zoom * 4
                Call render()
                'Print("zoom: " + Str$(1 / zoom))
            Case Keys.Oemplus
                If (1 / zoom) < 256 Then
                    zoom = zoom / 4
                End If
                Call render()
                'Print("zoom: " + Str$(1 / zoom))
            Case Keys.O
                drwaxis = drwaxis * -1
                Call render()
            Case Keys.P
                drwrul = drwrul * -1
                Call render()
            Case Keys.D
                Select Case drwmode
                    Case "line"
                        drwmode = "dot"
                    Case "dot"
                        drwmode = "line"
                End Select
                Call render()
            Case Keys.T
                Select Case trace
                    Case 0
                        trace = 1
                    Case 1
                        trace = 2
                    Case 2
                        trace = 3
                    Case 3
                        trace = 0
                End Select
                Call render()
            Case Keys.Oemcomma
                'Xtrace = Xtrace - ((xmin ^ 2 + xmax ^ 2) ^ (1 / 2)) / scnres.x
                Xtrace = Xtrace - stp.x / 4
                Call render()
            Case Keys.OemPeriod
                'Xtrace = Xtrace + ((xmin ^ 2 + xmax ^ 2) ^ (1 / 2)) / scnres.x
                Xtrace = Xtrace + stp.x / 4
                Call render()
            Case Keys.V
                '    Input("-- Enter a value for X:", Xtrace)
                Dialog1.ShowDialog()
                If Dialog1.DialogResult = Windows.Forms.DialogResult.OK Then
                    Xtrace = Val(Dialog1.TextBox1.Text)
                    render()
                End If
                Call render()
            Case Keys.I
                'Dialog1.ShowDialog()
                Dim fn1 As Integer, fn2 As Integer, ubnd As Double, lbnd As Double, tmp As Double
                Dialog3.ShowDialog()
                If Dialog3.DialogResult = Windows.Forms.DialogResult.OK Then
                    fn1 = Int(Val(Dialog3.TextBox1.Text))
                    fn2 = Int(Val(Dialog3.TextBox2.Text))
                    lbnd = Val(Dialog3.TextBox3.Text)
                    ubnd = Val(Dialog3.TextBox4.Text)
                    'render()
                    tmp = getintersection(fn1, fn2, lbnd, ubnd)
                    Me.PictureBox1.CreateGraphics.DrawString("F" + Str$(fn1) + "() and F" + Str$(fn2) + "() intersect at:" + Str$(tmp), Font, System.Drawing.Brushes.White, 0, 10)
                    dcircleS(cnvt(tmp, "x"), cnvt(F1(tmp), "y"), 10, RGB(127, 127, 127))
                End If


            Case Keys.R
                Dim fu As Integer, xv As Double, dr As Double
                Dim sr As String, ds As Double, dsstp As Double, dsstp2 As Double
                Dim xs As Double, ys As Double, an As Double
                Dialog2.ShowDialog()
                If Dialog2.DialogResult = Windows.Forms.DialogResult.OK Then
                    fu = Int(Val(Dialog2.TextBox1.Text))
                    xv = Val(Dialog2.TextBox2.Text)
                    render()

                    dr = getderivative(fu, xv)
                    an = Math.Atan(dr)
                    ds = dis(0, 0, stp.x, stp.y)
                    ys = ds * Math.Sin(an)
                    xs = ds * Math.Cos(an)

                    'Me.PictureBox1.CreateGraphics.DrawString("The derivative of F" + Str$(fu) + "(" + Str$(xv) + ") is :" + Str$(dr), Font, System.Drawing.Brushes.White, 0, 10)
                    Select Case fu
                        Case 1
                            Call dline(xv - xs, xv + xs, F1(xv) - ys, F1(xv) + ys, RGB(127, 127, 127))
                        Case 2
                            Call dline(xv - xs, xv + xs, F2(xv) - ys, F2(xv) + ys, RGB(127, 127, 127))
                        Case 2
                            Call dline(xv - xs, xv + xs, F3(xv) - ys, F3(xv) + ys, RGB(127, 127, 127))
                    End Select

                    'Print(s)
                End If
            Case Keys.W
                Dialog4.ShowDialog()
                If Dialog4.DialogResult = Windows.Forms.DialogResult.OK Then
                    window.xmin = Val(Dialog4.TextBox1.Text)
                    window.xmax = Val(Dialog4.TextBox2.Text)
                    window.ymin = Val(Dialog4.TextBox3.Text)
                    window.ymax = Val(Dialog4.TextBox4.Text)
                    stp.x = Val(Dialog4.TextBox5.Text)
                    stp.y = Val(Dialog4.TextBox6.Text)
                    Call render()
                End If
            Case Keys.F1
                df1 = df1 * -1
                Call render()
            Case Keys.F2
                df2 = df2 * -1
                Call render()
            Case Keys.F3
                df3 = df3 * -1
                Call render()
            Case Keys.Q
                Beep()
                dmode = "physics"
        End Select
        'font.Dispose()
    End Sub


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        trace = 0
        Xtrace = 0
        drwmode = "line"
        drwaxis = 1
        drwrul = 1
        one = 1
        'PI = 3.14
        PI = System.Math.Atan(one) * 4
        zoom = 1
        dmode = "function"
        scnres.x = Me.PictureBox1.Width
        scnres.y = Me.PictureBox1.Height
        window.xmin = -4
        window.xmax = 4
        window.ymin = -4
        window.ymax = 4
        w.xmin = window.xmin
        w.xmax = window.xmax
        w.ymin = window.ymin
        w.ymax = window.ymax



        stp.x = 1
        stp.y = 1
        nstp.x = stp.x
        nstp.y = stp.y

        df1 = 1
        df2 = 1
        df3 = 1
        v1.x = System.Math.Cos(PI / 3)
        v1.y = System.Math.Sin(PI / 3)
        v2.x = System.Math.Cos(((2 * PI) / 3) - 2 * PI)
        v2.y = System.Math.Sin(((2 * PI) / 3) - 2 * PI)
        dmode = "function"

        Call render()


    End Sub

 

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize, MyBase.SizeChanged
        'Me.PictureBox1.Height = Me.Height - 100
        'Me.PictureBox1.Width = Me.Width - 100
        scnres.x = Me.PictureBox1.Width
        scnres.y = Me.PictureBox1.Height
        render()

    End Sub
    Function cnvt#(ByVal vl As Double, ByVal xy As String)
        Dim t As Double
        Dim xcenter As Double, ycenter As Double
        xcenter = window.xmin + dis(window.xmin, 0, window.xmax, 0) / 2
        ycenter = window.ymin + dis(0, window.ymin, 0, window.ymax) / 2

        xmin = xcenter - (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        xmax = xcenter + (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        ymin = ycenter - (dis(0, window.ymin, 0, window.ymax) / 2) * zoom
        ymax = ycenter + (dis(0, window.ymin, 0, window.ymax) / 2) * zoom


        Select Case xy
            Case "x"
                t = scnres.x * (((-1 * xmin) + vl) / dis(xmin, 0, xmax, 0))
                Select Case t
                    Case Is < 0
                        cnvt = -1
                    Case Is > scnres.x
                        cnvt = scnres.x + 1
                    Case Else
                        cnvt = t
                End Select
            Case "y"
                t = scnres.y * (1 - ((-1 * ymin) + vl) / dis(ymin, 0, ymax, 0))
                Select Case t
                    Case Is < 0
                        cnvt = -1
                    Case Is > scnres.y
                        cnvt = scnres.y + 1
                    Case Else
                        cnvt = t
                End Select
        End Select
    End Function

    Sub daxis()


        Dim t As Double
        Dim xcenter As Double, ycenter As Double
        xcenter = window.xmin + dis(window.xmin, 0, window.xmax, 0) / 2
        ycenter = window.ymin + dis(0, window.ymin, 0, window.ymax) / 2

        xmin = xcenter - (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        xmax = xcenter + (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        ymin = ycenter - (dis(0, window.ymin, 0, window.ymax) / 2) * zoom
        ymax = ycenter + (dis(0, window.ymin, 0, window.ymax) / 2) * zoom



        REM draw x and y axis
        Dim lw As Integer
        Dim r As Integer, g As Double, b As Double
        Dim pn As New Pen(Color.Aqua)
        Dim cl As System.Drawing.Color
        'Dim perc As Double
        '      the width of the line notches
        lw = 2
        r = 255
        g = 255
        b = 255

        cl = System.Drawing.ColorTranslator.FromWin32(RGB(r, g, b))
        pn.Color = cl


        If drwaxis = 1 Then
            Call dline(xmin, xmax, 0, 0, RGB(255, 255, 255))
            Call dline(0, 0, ymin, ymax, RGB(255, 255, 255))
        End If
        If drwrul = 1 Then

            Dim I As Double
            For I = 0 To xmin Step fst(xmin, "x")
                Me.PictureBox1.CreateGraphics.DrawLine(pn, CInt(cnvt(I, "x")), CInt(cnvt(0, "y") - lw), CInt(cnvt(I, "x")), CInt(cnvt(0, "y") + lw))
            Next I
            For I = 0 To xmax Step fst(xmax, "x")
                Me.PictureBox1.CreateGraphics.DrawLine(pn, CInt(cnvt(I, "x")), CInt(cnvt(0, "y") - lw), CInt(cnvt(I, "x")), CInt(cnvt(0, "y") + lw))
            Next I
            For I = 0 To ymin Step fst(ymin, "y")
                Me.PictureBox1.CreateGraphics.DrawLine(pn, CInt(cnvt(0, "x") - lw), CInt(cnvt(I, "y")), CInt(cnvt(0, "x") + lw), CInt(cnvt(I, "y")))
            Next I
            For I = 0 To ymax Step fst(ymax, "y")
                Me.PictureBox1.CreateGraphics.DrawLine(pn, CInt(cnvt(0, "x") - lw), CInt(cnvt(I, "y")), CInt(cnvt(0, "x") + lw), CInt(cnvt(I, "y")))
            Next I
        End If
        pn.Dispose()
    End Sub



    Sub dfunc(ByVal f_1 As Integer, ByVal f_2 As Integer, ByVal f_3 As Integer)
        ' draw function
        Dim st As Double, I As Double
        st = (dis(xmin, 0, xmax, 0) / scnres.x)
        For I = xmin To xmax Step st

            If f_1 <> 1 Or F1(I) Like System.Double.NaN Or F1(I - st) Like System.Double.NaN Then GoTo skip1
            'If (F1(I - st) < ymin And F1(I) > ymax) Or (F1(I) < ymin And F1(I - st) > ymax) Or (F1(I) < ymax And F1(I) > ymin And (F1(I - st) < ymin Or F1(I - st) > ymax)) Or (F1(I - st) < ymax And F1(I - st) > ymin And (F1(I) < ymin Or F1(I) > ymax)) Then
            'don't draw function
            'Else
            Select Case drwmode
                Case "line"
                    Call dline(I, I - st, F1(I), F1(I - st), RGB(255, 0, 0))
                Case "dot"
                    Call dpoint(I, F1(I), RGB(255, 0, 0))
            End Select
            'End If
skip1:
            If f_2 <> 1 Or F2(I) Like System.Double.NaN Or F2(I - st) Like System.Double.NaN Then GoTo skip2
            'If f_2 = 1 And (F2(I) <> ymax + 1.2345678) And (F2(I - st) <> ymax + 1.2345678) Then
            'If (F2(I - st) < ymin And F2(I) > ymax) Or (F2(I) < ymin And F2(I - st) > ymax) Or (F2(I) < ymax And F2(I) > ymin And (F2(I - st) < ymin Or F2(I - st) > ymax)) Or (F2(I - st) < ymax And F2(I - st) > ymin And (F2(I) < ymin Or F2(I) > ymax)) Then
            'don't draw function
            'Else
            Select Case drwmode
                Case "line"
                    Call dline(I, I - st, F2(I), F2(I - st), RGB(0, 255, 0))
                Case "dot"
                    Call dpoint(I, F2(I), RGB(0, 255, 0))
            End Select
            'End If
            'End If
skip2:
            If f_3 <> 1 Or F3(I) Like System.Double.NaN Or F3(I - st) Like System.Double.NaN Then GoTo skip3
            'If f_3 = 1 And (F3(I) <> ymax + 1.2345678) And (F3(I - st) <> ymax + 1.2345678) Then
            'If (F3(I - st) < ymin And F3(I) > ymax) Or (F3(I) < ymin And F3(I - st) > ymax) Or (F3(I) < ymax And F3(I) > ymin And (F3(I - st) < ymin Or F3(I - st) > ymax)) Or (F3(I - st) < ymax And F3(I - st) > ymin And (F3(I) < ymin Or F3(I) > ymax)) Then
            'don't draw function
            'Else
            Select Case drwmode
                Case "line"
                    Call dline(I, I - st, F3(I), F3(I - st), RGB(0, 0, 255))
                Case "dot"
                    Call dpoint(I, F3(I), RGB(0, 0, 255))
            End Select
            'End If
            'End If
skip3:




        Next I

    End Sub



    Function dis#(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double)
        dis = Math.Sqrt((x1 - x2) ^ 2 + (y1 - y2) ^ 2)
    End Function

    Function dis2#(ByVal p1 As Double, ByVal p2 As Double)
        dis2 = Math.Sqrt((p1 - p2) * (p1 - p2))
    End Function

    Sub dline(ByVal x1 As Double, ByVal x2 As Double, ByVal y1 As Double, ByVal y2 As Double, ByVal c As Double)
        Dim xb1 As Integer, xb2 As Integer, yb1 As Integer, yb2 As Integer
        Dim p1 As vector, p2 As vector
        Dim m As Double
        p1.x = x1
        p1.y = y1
        p2.x = x2
        p2.y = y2

        'determin if either point is undefined, if so then exit
        If (p1.y Like Double.NaN Or p2.y Like Double.NaN) Then GoTo ext
        'determin if both points are outside of the window and in the same quadrant (if so then exit)
        If (p1.y > ymax And p2.y > ymax) Or (p1.y < ymin And p2.y < ymin) Then GoTo ext
        'determin if both points are at opposite infinite y values
        If (p1.y Like Double.PositiveInfinity And p2.y Like Double.NegativeInfinity) Or (p1.y Like Double.NegativeInfinity And p2.y Like Double.PositiveInfinity) Then GoTo ext
        'if one point is not at +- infinity and the other is then set the infinit point at the window min/max
        If (p1.y Like Double.PositiveInfinity) Then p1.y = ymax
        If (p1.y Like Double.NegativeInfinity) Then p1.y = ymin
        If (p2.y Like Double.PositiveInfinity) Then p2.y = ymax
        If (p2.y Like Double.NegativeInfinity) Then p2.y = ymin
        'keep the line inside of the window
        If (p1.y <= ymax And p1.y >= ymin And p1.x <= xmax And p1.x >= xmin) And (p2.y > ymax Or p2.y < ymin) Then

            If p2.y > ymax Then
                p2.y = ymax
            End If
            If p2.y < ymin Then
                p2.y = ymin
            End If

            'p2.x = x2 + (p1.y - p2.y / ((y1 - y2) / (x1 - x2)))
            p2.x = x1 + ((p2.y - p1.y) * (x2 - p1.x)) / (y2 - p1.y)

        End If
        If (p2.y <= ymax And p2.y >= ymin) And (p1.y > ymax Or p1.y < ymin) Then

            If p1.y > ymax Then
                p1.y = ymax
            End If
            If p1.y < ymin Then
                p1.y = ymin
            End If

            'p1.x = x1 + (p2.y - p1.y / ((y2 - y1) / (x2 - x1)))
            p1.x = x1 + ((p1.y - p2.y) * (x1 - p2.x)) / (y1 - p2.y)

        End If
        x1 = p1.x
        y1 = p1.y
        x2 = p2.x
        y2 = p2.y


        xb1 = CInt(cnvt(x1, "x"))
        xb2 = CInt(cnvt(x2, "x"))
        yb1 = CInt(cnvt(y1, "y"))
        yb2 = CInt(cnvt(y2, "y"))

        'Dim r As Integer, g As Double, b As Double
        Dim pn As New Pen(Color.Aqua)
        Dim cl As System.Drawing.Color
        'r = 255
        'g = 0
        'b = 0

        'Dim cl As System.Drawing.Color
        'cl = pn.Color.AliceBlue
        'pn.Color = cl2

        'cl = System.Drawing.ColorTranslator.FromWin32(RGB(r, g, b))
        cl = System.Drawing.ColorTranslator.FromWin32(c)

        pn.Color = cl

        'cl = System.Drawing.Color.FromArgb(127, 255 * (i / Me.PictureBox1.Width), 0, 0)
        'pn.Color = Color.AntiqueWhite 'System.Drawing.Color.FromArgb(1, 1, 1, 1)
        Me.PictureBox1.CreateGraphics.DrawLine(pn, xb1, yb1, xb2, yb2)

        pn.Dispose()






ext:
        'LINE (, CINT(cnvt(ln.y, "y")))-(CINT(cnvt(ln.xn, "x")), CINT(cnvt(ln.yn, "y"))), c
    End Sub

    Function dotp#(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal xy As String)
        Select Case xy
            Case "x"
                dotp = x1 * x2
            Case "y"
                dotp = y1 * y2
        End Select
    End Function

    Sub dpfunc()
        Dim st As Double
        Dim I As Double
        st = ((2 * PI) / 1000)
        For I = 0 To 2 * PI Step st
            If PF(I) < 0 Or PF(I - st) < 0 Then
                ' don't draw function
            Else
                Select Case drwmode
                    Case "line"
                        Call dline(PF(I - st) * System.Math.Cos(I - st), PF(I) * System.Math.Cos(I), PF(I - st) * System.Math.Sin(I - st), PF(I) * System.Math.Sin(I), RGB(127, 200, 100))
                    Case "dot"
                        Call dpoint(PF(I) * System.Math.Cos(I), PF(I) * System.Math.Sin(I), RGB(127, 200, 100))
                End Select
            End If
        Next I
    End Sub

    Sub dpoint(ByVal x As Double, ByVal y As Double, ByVal c As Integer)
        Dim rect As System.Drawing.Rectangle
        rect.Width = 1
        rect.Height = 1
        Dim xb As Integer, Yb As Integer
        xb = CInt(cnvt(x, "x"))
        Yb = CInt(cnvt(y, "y"))
        rect.X = xb
        rect.Y = Yb
        Dim pn As New Pen(Color.Aqua)
        Dim cl As System.Drawing.Color
        'Dim brush As System.Drawing.Brush
        cl = System.Drawing.ColorTranslator.FromWin32(c)
        pn.Color = cl
        Me.PictureBox1.CreateGraphics.DrawRectangle(pn, rect)

     pn.Dispose()


        'PSET (CINT(cnvt(p.x, "x")), CINT(cnvt(p.y, "y"))), p.c
    End Sub

    Sub dvector(ByVal v As vector, ByVal C As Integer)
        Call dline(0, v.x, 0, v.y, C)
    End Sub

    Function F1(ByVal x As Double) As Double
        'F1 = (3 * x + 4) * (2 * x - 3) / x
        F1 = (2 * x + 2) * (5 * x - 3) ^ 2 * x
    End Function

    Function F2#(ByVal x As Double)
      'F2 = (-(System.Math.Sqrt(25 * x ^ 2 + 100 * x + 91) + 9)) / 3
        'F2 = -x ^ 2 + 1
        'F2 = (2 * x - 8) * (-x + 4)
        'F2 = -x / Math.Sqrt(x)
        'F2 = 1 / Math.Cos(x)
        F2 = getderivative(1, x)
    End Function

    Function F3#(ByVal x As Double)
        'F3 = Math.Log(x)
        F3 = getderivative(2, x)
    End Function

    Function fst#(ByVal p As Double, ByVal xy As String)
        Select Case xy
            Case "x"
                If System.Math.Abs(p) <> p Then fst = -stp.x Else fst = stp.x
            Case "y"
                If System.Math.Abs(p) <> p Then fst = -stp.y Else fst = stp.y
        End Select
    End Function

    Function getderivative#(ByVal F As Integer, ByVal x As Double)
        Dim a As Double
        a = 10 ^ (-5)


        Select Case F
            Case 1
                getderivative = (F1(x) - F1(x + a)) / (x - (x + a))
            Case 2
                getderivative = (F2(x) - F2(x + a)) / (x - (x + a))
            Case 3
                getderivative = (F3(x) - F3(x + a)) / (x - (x + a))

        End Select
    End Function

    Function getintersection#(ByVal fn1 As Integer, ByVal fn2 As Integer, ByVal lbnd As Double, ByVal ubnd As Double)
        If ubnd < lbnd Then
            'Me.TextBox1.Text = ("---   The lower bound must be less then the upper bound! ---")
            GoTo ext
        End If

        Dim lw As Double
        Dim st As Double, ds2 As Double, I As Double, ds(2) As Double
        Dim f1b As Double, f1bb As Double, f2b As Double, f2bb As Double
        st = dis2(lbnd, ubnd) / 10000
        'st = 1
        ds(lw) = -1

        For I = lbnd To ubnd Step st
            Select Case fn1
                Case 1
                    f1b = F1(I)
                    f1bb = F1(Int(I * 1000) / 1000)

                Case 2
                    f1b = F2(I)
                    f1bb = F2(Int(I * 1000) / 1000)
                Case 3
                    f1b = F3(I)
                    f1bb = F3(Int(I * 1000) / 1000)
            End Select
            Select Case fn2
                Case 1
                    f2b = F1(I)
                    f2bb = F1(Int(I * 1000) / 1000)
                Case 2
                    f2b = F2(I)
                    f2bb = F2(Int(I * 1000) / 1000)
                Case 3
                    f2b = F3(I)
                    f2bb = F3(Int(I * 1000) / 1000)
            End Select
            'Call dline(I, I, f1b, f2b, RGB(127, 127, 127))

            If I = lbnd Then
                ds(0) = dis2(f1b, f2b)
                ds(lw) = I
                ds2 = dis2(f1b, f2b)
            End If

            If dis2(f1b, f2b) = 0 Then
                getintersection = I
                GoTo ext
            End If

            If ds2 > dis2(f1b, f2b) Then
                If dis2(cnvt(f1b, "y"), cnvt(f2b, "y")) <= dis2(ymin, xmax) Then
                    ds(0) = dis2(f1b, f2b)
                    ds(lw) = I
                    ds2 = dis2(f1b, f2b)
                End If
            End If
            If dis2(f1bb, f2bb) = 0 Then
                ds(0) = 0
                ds(lw) = (Int(I * 1000) / 1000)
                getintersection = ds(lw)
                GoTo ext
            End If
        Next

        getintersection = ds(lw)




ext:
    End Function



    Function PF#(ByVal x As Double)
        PF = (Int(x * 13) / 13) / 4
    End Function

    Sub render()

        Dim xcenter As Double, ycenter As Double
        xcenter = window.xmin + dis(window.xmin, 0, window.xmax, 0) / 2
        ycenter = window.ymin + dis(0, window.ymin, 0, window.ymax) / 2

        xmin = xcenter - (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        xmax = xcenter + (dis(window.xmin, 0, window.xmax, 0) / 2) * zoom
        ymin = ycenter - (dis(0, window.ymin, 0, window.ymax) / 2) * zoom
        ymax = ycenter + (dis(0, window.ymin, 0, window.ymax) / 2) * zoom

        'CLS()
        Me.PictureBox1.Refresh()
        Call daxis()

        Select Case dmode
            Case "vector"
                v3 = vmul(v1, v2)
                Call dvector(v1, RGB(255, 0, 0))
                Call dvector(v2, RGB(0, 255, 0))
                Call dvector(v3, RGB(0, 0, 255))
            Case "function"
                Call dfunc(df1, df2, df3)
                Me.PictureBox1.CreateGraphics.DrawString("zoom = " + Str$(1 / zoom), Font, System.Drawing.Brushes.White, 0, 20)
                Select Case trace
                    Case 1
                        dcircleS(cnvt(Xtrace, "x"), cnvt(F1(Xtrace), "y"), 10, RGB(0, 127, 127))
                        Me.PictureBox1.CreateGraphics.DrawString("X = " + Str$(Xtrace), Font, System.Drawing.Brushes.White, 0, 0)
                        Me.PictureBox1.CreateGraphics.DrawString("F1(x) = " + Str$(F1(Xtrace)), Font, System.Drawing.Brushes.White, 0, 10)
                    Case 2
                        dcircleS(cnvt(Xtrace, "x"), cnvt(F2(Xtrace), "y"), 10, RGB(0, 127, 127))
                        Me.PictureBox1.CreateGraphics.DrawString("X = " + Str$(Xtrace), Font, System.Drawing.Brushes.White, 0, 0)
                        Me.PictureBox1.CreateGraphics.DrawString("F2(x) = " + Str$(F2(Xtrace)), Font, System.Drawing.Brushes.White, 0, 10)
                    Case 3
                        dcircleS(cnvt(Xtrace, "x"), cnvt(F3(Xtrace), "y"), 10, RGB(0, 127, 127))
                        Me.PictureBox1.CreateGraphics.DrawString("X = " + Str$(Xtrace), Font, System.Drawing.Brushes.White, 0, 0)
                        Me.PictureBox1.CreateGraphics.DrawString("F3(x) = " + Str$(F3(Xtrace)), Font, System.Drawing.Brushes.White, 0, 10)



                End Select
                Me.PictureBox1.CreateGraphics.DrawString("F1(x)", Font, System.Drawing.Brushes.Red, 0, 30)
                Me.PictureBox1.CreateGraphics.DrawString("F2(x)", Font, System.Drawing.Brushes.Green, 0, 40)
                Me.PictureBox1.CreateGraphics.DrawString("F3(x)", Font, System.Drawing.Brushes.Blue, 0, 50)


            Case "polar"
                Call dpfunc()
            Case "physics"
                If BallMoving = True Then
                    BPos.x = BPos.xn
                    BPos.y = BPos.yn
                    BPos.x = BPos.x + BVol.x
                    BPos.y = BPos.y + BVol.y
                    If BPos.y < 0 Then
                        BallMoving = False
                    Else

                    End If
                    'Me.PictureBox1.CreateGraphics.DrawString("New X = " + Str$(MouseP.x), Font, System.Drawing.Brushes.White, 0, 0)
                    'Me.PictureBox1.CreateGraphics.DrawString("New Y = " + Str$(MouseP.y), Font, System.Drawing.Brushes.White, 0, 12)
                    Me.PictureBox1.CreateGraphics.DrawString("Time Interval = " + Str$(MouseT(1) - MouseT(2)), Font, System.Drawing.Brushes.White, 0, 20)
                    'Me.PictureBox1.CreateGraphics.DrawString("Mouse Y = " + Str$(cnvt2(MouseP.y, "y")), Font, System.Drawing.Brushes.White, 0, 30)
                    Me.PictureBox1.CreateGraphics.DrawString("Vol X = " + Str$(MouseV2.x), Font, System.Drawing.Brushes.White, 0, 40)
                    Me.PictureBox1.CreateGraphics.DrawString("Vol Y = " + Str$(MouseV2.y), Font, System.Drawing.Brushes.White, 0, 50)
                    Me.PictureBox1.CreateGraphics.DrawString("Ball Pos Y = " + Str$(BPos.y), Font, System.Drawing.Brushes.White, 0, 60)
                    Me.PictureBox1.CreateGraphics.DrawString("Ball Pos x = " + Str$(BPos.x), Font, System.Drawing.Brushes.White, 0, 70)


                End If


                'Me.PictureBox1.CreateGraphics.DrawString("time" + Str$(clock.TickCount), Font, System.Drawing.Brushes.White, 0, 60)
        End Select

        If dmode <> "physics" Then
            Me.PictureBox1.CreateGraphics.DrawString("xmax:" + Str$(xmax), Font, System.Drawing.Brushes.White, 0, 60)
            Me.PictureBox1.CreateGraphics.DrawString("xmin:" + Str$(xmin), Font, System.Drawing.Brushes.White, 0, 70)
            Me.PictureBox1.CreateGraphics.DrawString("ymin:" + Str$(ymin), Font, System.Drawing.Brushes.White, 0, 80)
            Me.PictureBox1.CreateGraphics.DrawString("ymax:" + Str$(ymax), Font, System.Drawing.Brushes.White, 0, 90)
            
        End If

    End Sub

    Function vadd(ByVal vv1 As vector, ByVal vv2 As vector) As vector
        Dim nv As vector
        nv.x = vv1.x + vv2.x
        nv.y = vv1.y + vv2.y
        vadd = nv
    End Function

    Function vsub(ByVal vv1 As vector, ByVal vv2 As vector) As vector
        Dim vn As vector
        vn.x = vv1.x - vv2.x
        vn.y = vv1.y - vv2.y
        vsub = vn
    End Function
    Function vmul(ByVal vv1 As vector, ByVal vv2 As vector) As vector
        Dim vn As vector
        vn.x = vv1.x * vv2.x
        vn.y = vv1.y * vv2.y
        vmul = vn
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Sub dcircleC(ByVal x As Double, ByVal y As Double, ByVal r As Double, ByVal c As Integer)

        If x Like Double.NaN Or y Like Double.NaN Then GoTo ext
        Dim pn As New Pen(Color.Aqua)
        Dim cl As System.Drawing.Color
        Dim rect As Drawing.Rectangle
        rect.Width = 2 * dis2(cnvt(0, "x"), cnvt(r, "x"))
        rect.Height = 2 * dis2(cnvt(0, "y"), cnvt(r, "y"))
        rect.X = cnvt(x - r, "x")
        rect.Y = cnvt(y + r, "y")
        cl = System.Drawing.ColorTranslator.FromWin32(c)
        pn.Color = cl
        Me.PictureBox1.CreateGraphics.DrawEllipse(pn, rect)
        pn.Dispose()
ext:
    End Sub
    Sub dcircleS(ByVal x As Double, ByVal y As Double, ByVal r As Double, ByVal c As Integer)

        If x Like Double.NaN Or y Like Double.NaN Then GoTo ext
        Dim pn As New Pen(Color.Aqua)
        Dim cl As System.Drawing.Color
        Dim rect As Drawing.Rectangle
        rect.Width = 2 * r
        rect.Height = 2 * r
        rect.X = x - r
        rect.Y = y - r
        cl = System.Drawing.ColorTranslator.FromWin32(c)
        pn.Color = cl
        Me.PictureBox1.CreateGraphics.DrawEllipse(pn, rect)
        pn.Dispose()
ext:
    End Sub
    Function cnvt2(ByVal vl As Double, ByVal xy As String)
        Select Case xy
            Case "x"
                'cnvt2 = dis(xmin, 0, xmax, 0) * vl + xmax
                cnvt2 = ((vl / PictureBox1.Width) * (Math.Abs(xmin) + Math.Abs(xmax))) + xmin
            Case "y"
                'cnvt2 = dis(ymin, 0, ymax, 0) * vl + ymax
                cnvt2 = -(((vl / PictureBox1.Height) * (Math.Abs(ymin) + Math.Abs(ymax))) + ymin)
            Case Else
                cnvt2 = Double.NaN
        End Select
    End Function


    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If dmode = "physics" Then
            MouseT(2) = MouseT(1)
            MouseT(1) = clock.TickCount
            MouseP.xn = MouseP.x
            MouseP.yn = MouseP.y
            MPoint.X = MousePosition.X
            MPoint.Y = MousePosition.Y
            MPoint = PictureBox1.PointToClient(MPoint)
            MouseP.x = MPoint.X
            MouseP.y = MPoint.Y
            MouseP2.x = cnvt2(MouseP.x, "x")
            MouseP2.y = cnvt2(MouseP.y, "y")
            MouseP2.xn = cnvt2(MouseP.xn, "x")
            MouseP2.yn = cnvt2(MouseP.yn, "y")


            'MouseV.x = (MouseP.x - MouseP.xn) / ((MouseT(1) - MouseT(2)) / 1000)
            'MouseV.y = (MouseP.y - MouseP.yn) / ((MouseT(1) - MouseT(2)) / 1000)
            'MouseV2.y = (MouseP.y - MouseP.yn)
            'MouseV2.x = (MouseP.x - MouseP.xn)
            MouseV2.x = (MouseP2.x - MouseP2.xn) / ((MouseT(1) - MouseT(2)) / 1000)
            MouseV2.y = (MouseP2.y - MouseP2.yn) / ((MouseT(1) - MouseT(2)) / 1000)
            Call render()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        BallMoving = True
        BVol.x = MouseV2.x
        BVol.y = -32
        BPos.x = MouseP2.x
        BPos.y = MouseP2.y
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EvaluateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EvaluateToolStripMenuItem.Click
        '    Input("-- Enter a value for X:", Xtrace)
        Dialog1.ShowDialog()
        If Dialog1.DialogResult = Windows.Forms.DialogResult.OK Then
            Xtrace = Val(Dialog1.TextBox1.Text)
            render()
        End If
        Call render()
    End Sub

    Private Sub DerivativeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DerivativeToolStripMenuItem.Click
        Dim fu As Integer, xv As Double, dr As Double
        Dim sr As String, ds As Double, dsstp As Double, dsstp2 As Double
        Dim xs As Double, ys As Double, an As Double
        Dialog2.ShowDialog()
        If Dialog2.DialogResult = Windows.Forms.DialogResult.OK Then
            fu = Int(Val(Dialog2.TextBox1.Text))
            xv = Val(Dialog2.TextBox2.Text)
            render()

            dr = getderivative(fu, xv)
            an = Math.Atan(dr)
            ds = dis(0, 0, stp.x, stp.y)
            ys = ds * Math.Sin(an)
            xs = ds * Math.Cos(an)

            'Me.PictureBox1.CreateGraphics.DrawString("The derivative of F" + Str$(fu) + "(" + Str$(xv) + ") is :" + Str$(dr), Font, System.Drawing.Brushes.White, 0, 10)
            Select Case fu
                Case 1
                    Call dline(xv - xs, xv + xs, F1(xv) - ys, F1(xv) + ys, RGB(127, 127, 127))
                Case 2
                    Call dline(xv - xs, xv + xs, F2(xv) - ys, F2(xv) + ys, RGB(127, 127, 127))
                Case 2
                    Call dline(xv - xs, xv + xs, F3(xv) - ys, F3(xv) + ys, RGB(127, 127, 127))
            End Select

            'Print(s)
        End If
    End Sub
End Class
