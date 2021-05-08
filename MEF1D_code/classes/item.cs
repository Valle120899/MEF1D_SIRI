using System;

namespace Classes
{

    //Clase abstracta que representa un objeto en la malla
    abstract class Item
    {
        protected int id; //identificador
        protected float x; //coordenada en X (basta con este dato por estar en 1 dimensión)
        protected int node1; //identificador de nodo
        protected int node2; //segundo identificador de nodo
        protected float value; //valor asociado al objeto

        //Getters para los atributos
        public int getId()
        {
            return id;
        }

        public float getX()
        {
            return x;
        }

        public int getNode1()
        {
            return node1;
        }

        public int getNode2()
        {
            return node2;
        }

        public float getValue()
        {
            return value;
        }

        //Métodos abstractos para instanciar los atributos de acuerdo a las necesidades

        //Caso en que se utiliza un entero y un real
        public abstract void setIntFloat(int n, float r);

        //Caso en que se utilizan tres enteros
        public abstract void setIntIntInt(int n1, int n2, int n3);
    }

    //Clase que representa cada nodo de la malla
    class node : Item
    {

        //Un nodo usa un entero y un real: su identificador, y su coordenada en X
        public override void setIntFloat(int identifier, float x_coordinate)
        {
            id = identifier;
            x = x_coordinate;
        }

        public override void setIntIntInt(int n1, int n2, int n3)
        {
        }

    };

    //Clase que representa un elemento en la malla
    class element : Item
    {

        public override void setIntFloat(int n1, float r)
        {
        }

        //Un elemento usa tres enteros: su identificador, y los identificadores de sus nodos
        public override void setIntIntInt(int identifier, int firstnode, int secondnode)
        {
            id = identifier;
            node1 = firstnode;
            node2 = secondnode;
        }

    };

    //Clase que representa una condición impuesta en un nodo de la malla
    class condition : Item
    {

        //Una condición usa un entero y un real: un identificador de nodo, y un valor a aplicar
        public override void setIntFloat(int node_to_apply, float prescribed_value)
        {
            node1 = node_to_apply;
            value = prescribed_value;
        }

        public override void setIntIntInt(int n1, int n2, int n3)
        {
        }

    };
}