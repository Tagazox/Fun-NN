

using System;
///This programme is for learn, represent a neural network.
///   Copyright(C) 2016 Hippolyte Nioche
///
///
///This program is free software; you can redistribute it and/or modify
///it under the terms of the GNU General Public License as published by
///the Free Software Foundation;
///This program is distributed in the hope that it will be useful,
///but WITHOUT ANY WARRANTY; without even the implied warranty of
///MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
///GNU General Public License for more details.
///You should have received a copy of the GNU General Public License.
using System.Collections.Generic;

namespace NN
{
    class LiaisonNN_Jeux
    {
        /// <summary>
        /// Permet la liaisonentre l'algorithme génétique les réseau de neurones et le jeux. c'est ici que les entiter 
        /// piocheron leur cerveau.
        /// </summary>
        Genetic_Algorithme GA;
        public List<ReseauDeNeurones> theIa
        {get;internal set;}
        public LiaisonNN_Jeux()
        {
            int nbSensor=5;
            theIa = new List<ReseauDeNeurones>();
            for (int i = 0; i < 150; i++)
                theIa.Add(new ReseauDeNeurones(3 * nbSensor, 2, 1, 4 * nbSensor));
            GA = new Genetic_Algorithme(3 * nbSensor, 2, theIa, 1, 4 * nbSensor);
        }
        public ReseauDeNeurones Creatachildren(ReseauDeNeurones his)
        {
            return new ReseauDeNeurones(GA.Naissance(his));
        }
        public void iterate()
        {
            GA.EvaluationDesIndividus();
            GA.CreatioDesnouveauInddividues();
        }

        internal ReseauDeNeurones NewBrain()
        {
            theIa.Add(new ReseauDeNeurones(3 , 2, 1, 4 ));
            return theIa[theIa.Count - 1];
        }
        public ReseauDeNeurones getTheBest()
        {
            return new ReseauDeNeurones(GA.Best);
        }
    }
}
