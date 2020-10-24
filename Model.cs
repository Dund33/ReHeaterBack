using System;

public class Obiekt
{
    float Tp = 25;
    float Kcp;
    float Kcw;

    public float Simul(float TzewN,float Tzew, float TwewN, float QgN, float Qg) //N-nominalne wartości potrzebne do obliczenia przepuszczalności cieplnej reszta to zwykłe dane Tzew - temperatura na zewnątrz , Twew - temperatura wewnątrz, Qg - moc grzejnika 
    {
        Kcp = (QgN * (TzewN - Tp)) / ((Tp - TwewN) * (3 * Tp + TwewN - 4 * TzewN));
        Kcw = (QgN * (TwewN - Tp) * (TzewN - Tp)) / ((Tp - TwewN) * (Tp - TzewN) * (3 * Tp + TwewN - 4 * TzewN));
        float Twew = (Qg - (Tzew * Kcw) - (Tp * Kcp)) / (Kcw + Kcp);
        return Twew;
    }
}
