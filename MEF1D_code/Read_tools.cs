using System;
using System.IO;
//Se define un arreglo de reales como una lista
//Se define un arreglo de vectores como una matriz

namespace MEF1D_code
{
    public static class Read_tools
    {
        
        //Esta función recibe:
        //- Un flujo de archivo de texto para extraer la información
        //- La cantidad de líneas a omitir (puede ser 1 o 2)
        //- La cantidad de filas de información a extraer
        //- El arreglo de objetos a llenar con la información extraida
        //La función extrae del archivo de texto los datos de interés en la malla
        //de acuerdo a los parámetros enviados.
        
        public static void obtenerDatos(ref StreamReader file,int nlines,int n,int mode,Item[] item_list){
            //Se prepara una variable string para leer las líneas a omitir
            string line;
            //Siempre se omite al menos una línea
            line = file.ReadLine();
            line = file.ReadLine();
            //Si es necesario omitir una línea adicional, se efectúa
            if(nlines==((int) linesE.DOUBLELINE)) file.ReadLine();
            //Se itera una cantidad de veces igual a la cantidad de datos a extraer
            //que será igual a la cantidad de objetos a instanciar
            for(int i=0;i<n;i++){
                //Se verifica la cantidad y tipos de datos a extraer por fila
                switch(mode){
                //Se extrae un entero y un real por fila
                case ((int) modesE.INT_FLOAT):
                    int e; float r;
                    string phrase = file.ReadLine();
                    string[] words = phrase.Split('\t');
                    
                    e = Int32.Parse(words[0]); 
                    r = float.Parse(words[1]);
                    //Se instancian el entero y el real del objeto actual
                    item_list[i].setIntFloat(e,r);
                    break;
                //Se extraen tres enteros
                case (int) modesE.INT_INT_INT:
                    int e1,e2,e3;
                    string phrase2 = file.ReadLine();
                    string[] words2 = phrase2.Split(' ');
                    e1 = Int32.Parse(words2[0]);
                    e2 = Int32.Parse(words2[1]);
                    e3 = Int32.Parse(words2[2]);
                    //Se instancia los tres enteros en el objeto actual
                    item_list[i].setIntIntInt(e1,e2,e3);
                    break;
                }
            }
        }

        //Esta función recibe:
        //- El objeto mesh con toda la información de la malla
        //La función solicita el nombre del archivo que contiene la información de la malla
        //y procede a extraer todos los datos para colocarlos adecuadamente dentro del objeto mesh
        public static void leerMallayCondiciones(ref mesh m){
            //Se prepara un arreglo para el nombre del archivo
            String filename;

            //Se prepara un flujo para el archivo
            StreamReader file = null;
            //Se preparan variables para extraer los parámetros del problema y las cantidades de
            //datos en la malla (nodos, elementos, condiciones de Dirichlet, condiciones de Neumann)
            float l,k,Q;
            int nnodes,neltos,ndirich,nneu;

            //Se obliga al usuario a ingresar correctamente el nombre del archivo
            do{
                Console.WriteLine("Ingrese el nombre del archivo que contiene los datos de la malla: ");
                filename = Console.ReadLine();
                //Se intenta abrir el archivo
                file = new StreamReader(filename);
            }while(file.Equals(null)); //Si no fue posible abrir el archivo, se intenta de nuevo

            //Se leen y guardan los parámetros y cantidades
            string phrase = file.ReadLine();
            string[] words = phrase.Split(' ');
            l = float.Parse(words[0]);
            k = float.Parse(words[1]);
            Q = float.Parse(words[2]);

            string phrase2 = file.ReadLine();
            string[] words2 = phrase2.Split(' ');
            nnodes = Int32.Parse(words2[0]);
            neltos = Int32.Parse(words2[1]);
            ndirich = Int32.Parse(words2[2]);
            nneu = Int32.Parse(words2[3]);

            //Se instancian los parámetros y cantidades en el objeto mesh
            m.setParameters(l,k,Q);
            m.setSizes(nnodes,neltos,ndirich,nneu);
            //En base a las cantidades, se preparan arreglos de objetos para guardar
            //el resto de la información
            m.createData();
            //Se extraen, siguiendo el formato del archivo, la información de:
            //- Los nodos de la malla
            //- Los elementos de la malla
            //- Las condiciones de Dirichlet impuestas
            //- Las condiciones de Neumann impuestas
            obtenerDatos(ref file,(int) linesE.SINGLELINE,nnodes,(int) modesE.INT_FLOAT,m.getNodes());
            obtenerDatos(ref file,(int) linesE.DOUBLELINE,neltos,(int) modesE.INT_INT_INT,m.getElements());
            obtenerDatos(ref file,(int) linesE.DOUBLELINE,ndirich,(int) modesE.INT_FLOAT,m.getDirichlet());
            obtenerDatos(ref file,(int) linesE.DOUBLELINE,nneu,(int) modesE.INT_FLOAT,m.getNeumann());

            //Se cierra el archivo antes de terminar
            file.Close();
        }


    }
}