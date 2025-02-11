        public class Point
        {
            public double x;
            public double y;
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public void Derivative(string formula)
        {
            double x = 0.0;
            double y = 0.0;
            double z = 0.0;
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables["x"] = x;
            context.Variables["y"] = y;
            context.Variables["z"] = z;
            IDynamicExpression eDynamic = context.CompileDynamic(formula);
            IGenericExpression<double> eGeneric = context.CompileGeneric<double>(formula);
            double res = (double)eDynamic.Evaluate();
            res = eGeneric.Evaluate();
            List<List<double>> table = new List<List<double>>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    table[i][j] = 1;
                }
            }
        }

        public string SysUravneniy(double x, double y, double eps, string formulaX, string formulaY)
        {
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables["x"] = x;
            context.Variables["y"] = y;

            IDynamicExpression eDynamicX = context.CompileDynamic(formulaX);
            IGenericExpression<double> eGenericX = context.CompileGeneric<double>(formulaX);
            double resX = (double)eDynamicX.Evaluate();
            resX = eGenericX.Evaluate();

            IDynamicExpression eDynamicY = context.CompileDynamic(formulaY);
            IGenericExpression<double> eGenericY = context.CompileGeneric<double>(formulaY);
            double resY = (double)eDynamicY.Evaluate();
            resY = eGenericY.Evaluate();

            double xtmp = 0;
            double ytmp = 0;
            do
            {
                xtmp = x;
                ytmp = y;
                context.Variables["x"] = x;
                context.Variables["y"] = y;
                resX = eGenericX.Evaluate();
                resY = eGenericY.Evaluate();
                x = resX;
                y = resY;
            } 
            while ((Math.Abs(xtmp - x) > eps)||(Math.Abs(ytmp-y)>eps));

            string s = "x = " + x + " ; y = " + y + " ;"; 

            return s;
        }
        public double SoloUravnenie(double a, double b, double eps, string formula)
        {
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables["x"] = 0.0;
            IDynamicExpression eDynamic = context.CompileDynamic(formula);
            IGenericExpression<double> eGeneric = context.CompileGeneric<double>(formula);

            // Evaluate the expressions
            double result = (double)eDynamic.Evaluate();
            result = eGeneric.Evaluate();

            context.Variables["x"] = a;
            result = eGeneric.Evaluate();
            double res_a = result;
            context.Variables["x"] = b;
            result = eGeneric.Evaluate();
            double res_b = result;

            double x;
            double tmp;

            do
            {
                x = a;
                a = a - (res_a * (b - a)) / (res_b - res_a);
                tmp = a;
                context.Variables["x"] = a;
                result = eGeneric.Evaluate();
                res_a = result;

            } while (Math.Abs(x - tmp) > eps);
            return a;
        }
        public double F(double x, double y, string formula)
        {
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables["x"] = x;
            context.Variables["y"] = y;

            
            IGenericExpression<double> eGeneric = context.CompileGeneric<double>(formula + "+x*0");
            //IDynamicExpression eDynamic = context.CompileDynamic(formula + "+x*0");
            //double result = (double)eDynamic.Evaluate();
            //result = eGeneric.Evaluate();
            //return result;
            return eGeneric.Evaluate();
        }
        //Метод рассчёта функции, используя строку, как формулу по которой будет произведён рассчёт.