using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class template : ExpLibrary
{
    IEnumerator template1()
    {
        yield return new WaitForFixedUpdate();
        yield return StartCoroutine(turnCoroutine(50, 70));
        yield return StartCoroutine(waitCoroutine(10));
    }

}

public class code0class : ExpLibrary
{
    // code info
    public static void setCode0Info()
    {
        setup.codeActive[0] = false;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }



    public IEnumerator code0()
    {
        
        // PARTICIPANT CODE THAT LOOPS:
        if (reflectionDown() < 42)
        {
            yield return StartCoroutine(turnCoroutine(60, 110));
        }
        else
            motor(100, 100);

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code0");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code1class : code0class
{
    // code info
    public static void setCode1Info()
    {
        setup.codeActive[0] = false;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }
    
    public IEnumerator precode1()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:
        yield return new WaitForFixedUpdate();
    }
    

    public IEnumerator code1()
    {
        // PARTICIPANT CODE THAT LOOPS:
        
        if (ultrasound() > 120 && reflectionDown() > 42)
        {
            motor(100, -100);
        }
        else
        {
            if (reflectionDown() > 42)
            {
                motor(100, 100);
                yield return StartCoroutine(waitCoroutine(200));
            }
            else
            {
                Debug.Log("else");
                yield return StartCoroutine(waitCoroutine(200));
                motor(0, 0);
                yield return StartCoroutine(waitCoroutine(200));
                motor(-100, -100);
                yield return StartCoroutine(waitCoroutine(500));
                //yield return StartCoroutine(turnCoroutine(60, 130));
            }
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code1");
                setup.setCodeStillRunning(true);
            }     
        }
    }
}   // NordDesign2018 code
public class code2class : code1class
{
    // code info
    public static void setCode2Info()
    {
        setup.codeActive[1] = false;
        // setup.eval = 99;
        //setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.codename = "bareUltra_unperturbed";
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode2()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code2()
    {
        // PARTICIPANT CODE HERE:
        if (ultrasound() < 100)
        {
            motor(100, 100);
        }
        else
            motor(100, -100);

        if (ultrasound() < 15)
        {
            if (reflectionRedLeft() > 6 || reflectionRedRight() > 6)
                PlayTone(400, 100);
            else if (blink > 15)
                PlayTone(800, 100);
            else
                PlayTone(1600, 100);
        }



        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code2");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // bareUltra
public class code3class : code2class
{
    // code info
    public static void setCode3Info()
    {
        setup.codeActive[2] = false;
        // setup.eval = 99;
        //setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.codename = "p46eval5_unperturbed";
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int ultrasound_LP()
    {
        int u1 = ultrasound();
        int u2 = ultrasound();
        int u3 = ultrasound();
        int u4 = ultrasound();
        int u5 = ultrasound();
        return (u1 + u2 + u3 + u4 + u5) / 5;
    }

    public IEnumerator precode3()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code3()
    {
        // PARTICIPANT CODE HERE:

        motor(30, -30);
        while (ultrasound_LP() > 100)
        {
            //
            yield return new WaitForFixedUpdate();
        }
        motor(0, 0);
        //turn(60, 5);
        yield return StartCoroutine(turnCoroutine(60, 5));
        int turn_angle = 20 - ultrasound_LP() / 12;
        //turn(60, turn_angle);
        yield return StartCoroutine(turnCoroutine(60, turn_angle));


        motor(100, 100);
        while (reflectionDown() > 42)
        {
            // do nothing
            yield return new WaitForFixedUpdate();
        }
        motor(0, 0);


        if (ultrasound_LP() < 30)
        {

            if (reflectionRedLeft() > 10 || reflectionRedRight() > 10)
            {
                PlayTone(400, 2000);
            }

            else
            {
                //wait(2000);
                yield return StartCoroutine(waitCoroutine(2000));
                if (blink > 40)
                {
                    PlayTone(800, 2000);
                }
                else
                {
                    PlayTone(1600, 2000);
                }
            }

            motor(100, 100);
            //wait(500);
            yield return StartCoroutine(waitCoroutine(500));
            motor(-100, -100);
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(1000));
            motor(0, 0);
            //turn(60, 60);
            yield return StartCoroutine(turnCoroutine(60, 60));
        }


        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code3");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // p46eval5
public class code4class : code3class
{
    // code info
    public static void setCode4Info()
    {
        setup.codeActive[3] = true;
        // setup.eval = 99;
        //setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.codename = "blink_unperturbed";
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode4()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code4()
    {
        // PARTICIPANT CODE HERE:

        for (int i = 0; i < 18; i++)
        {
            //dispNum(0, 0, blink);
            //Debug.Log("for: " + blink);
            //turn(60, 20);
            yield return StartCoroutine(turnCoroutine(60, 20));
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(1000));
            if (blink >= 14)
            {
                //Debug.Log("break engaged");
                //PlayTone(440, 100);
                //turn(60, 20);
                yield return StartCoroutine(turnCoroutine(60, 20));
                //wait(1000);
                yield return StartCoroutine(waitCoroutine(1000));
                yield return new WaitForFixedUpdate();
                //Debug.Log("for2: " + blink);
                break;
            }
        }



        startTimer1();

        while (reflectionDown() > 42 && readTimer1() < 1000)
        {

            if (blink < 7 && readTimer1() < 1000)
            {
                //Debug.Log("white search " + blink);
                //Debug.Log("timer " + readTimer1());
                //Debug.Log("blink < 7");
                //turn(60, 20);
                yield return StartCoroutine(turnCoroutine(60, 20));
                //wait(800);
                yield return StartCoroutine(waitCoroutine(800));
            }
            else
            {
                //Debug.Log("push" + blink);
                motor(100, 100);
                //PlayTone(880, 100);
                startTimer1();
            }
            yield return new WaitForFixedUpdate();
        }

        //Debug.Log("timer ran out");

        startTimer2();
        while (reflectionDown() > 42 && readTimer2() < 2000)
        {
            //Debug.Log("driving free");
            motor(100, 100);
            yield return new WaitForFixedUpdate();
        }


        //wait(300);
        yield return StartCoroutine(waitCoroutine(300));
        motor(0, 0);
        //wait(300);
        yield return StartCoroutine(waitCoroutine(300));

        if (reflectionDown() < 42)
        {
            //Debug.Log("cardboard");
            //PlayTone(1200, 100);
            motor(-100, -100);
            //wait(300);
            yield return StartCoroutine(waitCoroutine(300));
            while (reflectionDown() < 42)
            {
                motor(100, -100);
                yield return new WaitForFixedUpdate();
            }
            //turn(60, 90);
            yield return StartCoroutine(turnCoroutine(60, 90));
            motor(100, 100);
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(300));
            motor(0, 0);
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code4");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // blink
public class code5class : code4class
{
    // code info
    public static void setCode5Info()
    {
        setup.codeActive[4] = false;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode5()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code5()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code5");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code6class : code5class
{
    // code info
    public static void setCode6Info()
    {
        setup.codeActive[5] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int intvar;

    public IEnumerator precode6()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code6()
    {

        // PARTICIPANT CODE HERE:
        PlayTone(1840, 400);
        //spin()
        int ang = 0;
        while (ang < 360)
        {
            ang = ang + 30;
            //turn(60, 30);
            yield return StartCoroutine(turnCoroutine(60, 30));
            //wait(700);
            yield return StartCoroutine(waitCoroutine(700));
            if (blink >= 20)
            {
                PlayTone(440, 300);
                intvar = 1;
                goto jump1;
            }
            yield return new WaitForFixedUpdate();
        }
        intvar = 0;
        jump1:
        int block = intvar;//spins until it finds a blinking light
        if (block == 0)
        {
            //newLocation();
            //turn(60, 120);
            yield return StartCoroutine(turnCoroutine(60, 120));
            motor(60, 60);
            startTimer1();
            while (true)
            {
                int time = readTimer1();
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                }
                if (time >= 5000)
                {
                    goto jump2;
                }
                yield return new WaitForFixedUpdate();
            }
            jump2:
            yield return new WaitForFixedUpdate();
        }
        if (block == 1)
        {
            PlayTone(840, 400);
            motor(60, 60);
            while (true)
            {
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                    //wait(1000);
                    yield return StartCoroutine(waitCoroutine(1000));
                    motor(0, 0);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code6");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // p69 eval1
public class code7class : code6class
{
    // code info
    public static void setCode7Info()
    {
        setup.codeActive[6] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int intvar = 0;

    public IEnumerator precode7()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code7()
    {
        // PARTICIPANT CODE HERE:
        PlayTone(1840, 400);
        int ang = 0;
        while (ang < 360)
        {
            ang = ang + 30;
            //turn(60, 30);
            yield return StartCoroutine(turnCoroutine(60, 30));
            //wait(700);
            yield return StartCoroutine(waitCoroutine(700));
            if (blink >= 20)
            {
                PlayTone(440, 300);
                intvar = 1;
                goto jump1;
            }
            yield return new WaitForFixedUpdate();
        }
        jump1:
        int block = intvar;//spins until it finds a blinking light
        if (block == 0)
        {
            //newLocation();
            //turn(60, 120);
            yield return StartCoroutine(turnCoroutine(60, 120));
            motor(60, 60);
            startTimer1();
            while (true)
            {
                int time = readTimer1();
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                }
                if (time >= 5000)
                {
                    goto jump2;
                }
                yield return new WaitForFixedUpdate();
            }
            jump2:
            yield return new WaitForFixedUpdate();
        }
        if (block == 1)
        {
            PlayTone(840, 400);
            motor(60, 60);
            startTimer2();
            while (true)
            {
                if (blink >= 20 && readTimer2() >= 2000)
                {
                    block = 0;
                    //turn(60, 180);
                    yield return StartCoroutine(turnCoroutine(60, 180));
                    break;
                }
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                    //wait(1000);
                    yield return StartCoroutine(waitCoroutine(1000));
                    motor(0, 0);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code7");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // p69 eval2
public class code8class : code7class
{
    // code info
    public static void setCode8Info()
    {
        setup.codeActive[7] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int intvar = 0;

    public IEnumerator precode8()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code8()
    {
        // PARTICIPANT CODE HERE:
        PlayTone(1840, 400);
        int ang = 0;

        while (ang < 360)
        {
            ang = ang + 30;
            //turn(60, 30);
            yield return StartCoroutine(turnCoroutine(60, 30));
            //wait(700);
            yield return StartCoroutine(waitCoroutine(700));
            if (blink >= 20)
            {
                PlayTone(440, 300);
                intvar = 1;
                goto jump1;
            }
            yield return new WaitForFixedUpdate();
        }
        jump1:
        int block = intvar;
        //spins until it finds a blinking light
        if (block == 0)
        {
            //newLocation();
            //turn(60, 120);
            yield return StartCoroutine(turnCoroutine(60, 120));
            motor(60, 60);
            startTimer1();
            while (true)
            {
                int time = readTimer1();
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                }
                if (time >= 5000)
                {
                    goto jump2;
                }
                yield return new WaitForFixedUpdate();
            }
            jump2:
            yield return new WaitForFixedUpdate();
        }
        if (block == 1)
        {
            PlayTone(840, 400);
            motor(60, 60);
            startTimer2();
            while (true)
            {
                if (ultrasound() <= 15)
                {
                    int reflection = reflectionRedLeft() + reflectionRedRight();
                    if (reflection >= 30)
                    {
                        PlayTone(400, 400);
                    }
                }
                if (blink <= 20 && readTimer2() >= 2000)
                {
                    block = 0;
                    //turn(60, 180);
                    yield return StartCoroutine(turnCoroutine(60, 180));
                    break;
                }
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                    //wait(1000);
                    yield return StartCoroutine(waitCoroutine(1000));
                    motor(0, 0);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code8");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // p69 eval3
public class code9class : code8class
{
    // code info
    public static void setCode9Info()
    {
        setup.codeActive[8] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int intvar = 0;

    public IEnumerator precode9()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code9()
    {
        // PARTICIPANT CODE HERE:
        int ang = 0;
        while (ang < 360)
        {
            ang = ang + 30;
            //turn(60, 30);
            yield return StartCoroutine(turnCoroutine(60, 30));
            //wait(700);
            yield return StartCoroutine(waitCoroutine(700));
            if (blink >= 20)
            {
                //PlayTone(440,300);
                intvar = 1;
                goto jump1;
            }
            yield return new WaitForFixedUpdate();
        }
        jump1:
        int block = intvar;//spins until it finds a blinking light
        if (block == 0)
        {
            //turn(60, 90);
            yield return StartCoroutine(turnCoroutine(60, 90));
            motor(60, 60);
            startTimer1();
            while (true)
            {
                int time = readTimer1();
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                }
                if (blink >= 20)
                {
                    goto jump2;
                }
                if (time >= 5000)
                {
                    goto jump2;
                }
                yield return new WaitForFixedUpdate();
            }
            jump2:
            yield return new WaitForFixedUpdate();
        }
        if (block == 1)
        {
            //PlayTone(840,400);
            motor(60, 60);
            startTimer2();
            while (true)
            {
                if (ultrasound() <= 15)
                {
                    int reflection = reflectionRedLeft() + reflectionRedRight();
                    if (reflection >= 30)
                    {
                        PlayTone(400, 400);
                    }
                }
                if (blink <= 25 && readTimer2() >= 1000)
                {
                    //reverse();
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    block = 0;
                    //turn(60, 180);
                    yield return StartCoroutine(turnCoroutine(60, 180));
                    break;
                }
                if (reflectionDown() >= 35 && reflectionDown() <= 39)
                {
                    //reverse();
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 120);
                    yield return StartCoroutine(turnCoroutine(60, 120));
                    motor(60, 60);
                    //wait(1000);
                    yield return StartCoroutine(waitCoroutine(1000));
                    motor(0, 0);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code9");
                setup.setCodeStillRunning(true);
            }
        }
    }
}   // p69 eval4
public class code10class : code9class
{
    // code info
    public static void setCode10Info()
    {
        setup.codeActive[9] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    int intvar = 0;

    public IEnumerator precode10()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code10()
    {
        // PARTICIPANT CODE HERE:
        int ang = 0;
        while (ang < 360)
        {
            ang = ang + 30;
            //turn(60, 30);
            yield return StartCoroutine(turnCoroutine(60, 30));
            //wait(700);
            yield return StartCoroutine(waitCoroutine(700));
            if (blink >= 20)
            {
                //PlayTone(440,300);
                intvar = 1;
                goto jump1;
            }
            yield return new WaitForFixedUpdate();
        }
        jump1:
        int block = intvar;//spins until it finds a blinking light
        if (block == 0)
        {
            //newLocation();
            //turn(60, 120);
            yield return StartCoroutine(turnCoroutine(60, 120));
            motor(60, 60);
            startTimer1();
            while (true)
            {
                int time = readTimer1();
                if (reflectionDown() >= 36 && reflectionDown() <= 39)
                {
                    motor(-40, -40);
                    //wait(500);
                    yield return StartCoroutine(waitCoroutine(500));
                    motor(0, 0);
                    //turn(60, 90);
                    yield return StartCoroutine(turnCoroutine(60, 90));
                    motor(60, 60);
                }
                if (blink >= 20)
                {
                    goto jump2;
                }
                if (time >= 5000)
                {
                    goto jump2;
                }
                yield return new WaitForFixedUpdate();
            }
            jump2:
            yield return new WaitForFixedUpdate();
        }
        if (block == 1)
        {
            //PlayTone(840,400);
            motor(60, 60);
            startTimer2();
            while (true)
            {
                if (ultrasound() <= 15)
                {
                    //blockColor();
                    int reflection = reflectionRedLeft() + reflectionRedRight();
                    if (reflection >= 30)
                    {
                        PlayTone(400, 400);
                    }
                }
                if (blink <= 25 && readTimer2() >= 1000)
                {
                    motor(-60, -60);
                    //wait(800);
                    yield return StartCoroutine(waitCoroutine(800));
                    motor(0, 0);
                    block = 0;
                    //turn(60, 180);
                    yield return StartCoroutine(turnCoroutine(60, 180));
                    break;
                }
                if (reflectionDown() >= 36 && reflectionDown() <= 39)
                {
                    //reverse();
                    //turn(60,120);
                    //motor(60,60);
                    motor(-60, -60);
                    //wait(800);
                    yield return StartCoroutine(waitCoroutine(800));
                    motor(0, 0);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code10");
                setup.setCodeStillRunning(true);
            }
        }
    }
}  // p69 eval5 

public class code11class : code10class
{
    // code info
    public static void setCode11Info()
    {
        setup.codeActive[10] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode11()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code11()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code11");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code12class : code11class
{
    // code info
    public static void setCode12Info()
    {
        setup.codeActive[11] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode12()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code12()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code12");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code13class : code12class
{
    // code info
    public static void setCode13Info()
    {
        setup.codeActive[12] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode13()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code13()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code13");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code14class : code13class
{
    // code info
    public static void setCode14Info()
    {
        setup.codeActive[13] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode14()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code14()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code14");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code15class : code14class
{
    // code info
    public static void setCode15Info()
    {
        setup.codeActive[14] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode15()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code15()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code15");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code16class : code15class
{
    // code info
    public static void setCode16Info()
    {
        setup.codeActive[15] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode16()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code16()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code16");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code17class : code16class
{
    // code info
    public static void setCode17Info()
    {
        setup.codeActive[16] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode17()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code17()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code17");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code18class : code17class
{
    // code info
    public static void setCode18Info()
    {
        setup.codeActive[17] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode18()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code18()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code18");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code19class : code18class
{
    // code info
    public static void setCode19Info()
    {
        setup.codeActive[18] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode19()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code19()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code19");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code20class : code19class
{
    // code info
    public static void setCode20Info()
    {
        setup.codeActive[19] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode20()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code20()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code20");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code21class : code20class
{
    // code info
    public static void setCode21Info()
    {
        setup.codeActive[20] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode21()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code21()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code21");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code22class : code21class
{
    // code info
    public static void setCode22Info()
    {
        setup.codeActive[21] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode22()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code22()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code22");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code23class : code22class
{
    // code info
    public static void setCode23Info()
    {
        setup.codeActive[22] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode23()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code23()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code23");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code24class : code23class
{
    // code info
    public static void setCode24Info()
    {
        setup.codeActive[23] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode24()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code24()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code24");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code25class : code24class
{
    // code info
    public static void setCode25Info()
    {
        setup.codeActive[24] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode25()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code25()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code25");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code26class : code25class
{
    // code info
    public static void setCode26Info()
    {
        setup.codeActive[25] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode26()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code26()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code26");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code27class : code26class
{
    // code info
    public static void setCode27Info()
    {
        setup.codeActive[26] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode27()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code27()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code27");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code28class : code27class
{
    // code info
    public static void setCode28Info()
    {
        setup.codeActive[27] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode28()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code28()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code28");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code29class : code28class
{
    // code info
    public static void setCode29Info()
    {
        setup.codeActive[28] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode29()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code29()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code29");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code30class : code29class
{
    // code info
    public static void setCode30Info()
    {
        setup.codeActive[29] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode30()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code30()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code30");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code31class : code30class
{
    // code info
    public static void setCode31Info()
    {
        setup.codeActive[30] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode31()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code31()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code31");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code32class : code31class
{
    // code info
    public static void setCode32Info()
    {
        setup.codeActive[31] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode32()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code32()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code32");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code33class : code32class
{
    // code info
    public static void setCode33Info()
    {
        setup.codeActive[32] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode33()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code33()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code33");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code34class : code33class
{
    // code info
    public static void setCode34Info()
    {
        setup.codeActive[33] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode34()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code34()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code34");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code35class : code34class
{
    // code info
    public static void setCode35Info()
    {
        setup.codeActive[34] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode35()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code35()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code35");
                setup.setCodeStillRunning(true);
            }
        }
    }
}

public class code36class : code35class
{
    // code info
    public static void setCode36Info()
    {
        setup.codeActive[35] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode36()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code36()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code36");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code37class : code36class
{
    // code info
    public static void setCode37Info()
    {
        setup.codeActive[36] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode37()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code37()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code37");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code38class : code37class
{
    // code info
    public static void setCode38Info()
    {
        setup.codeActive[37] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode38()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code38()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code38");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code39class : code38class
{
    // code info
    public static void setCode39Info()
    {
        setup.codeActive[38] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode39()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code39()
    {
        // PARTICIPANT CODE HERE:


        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code39");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
public class code40class : code39class
{
    // code info
    public static void setCode40Info()
    {
        setup.codeActive[39] = true;
        // setup.eval = 99;
        setup.codename = "p" + config.participantID + "eval" + setup.eval;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "";
    }

    public IEnumerator precode40()
    {
        // PARTICIPANT CODE THAT ONLY GETS EXECUTED ONCE:

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator code40()
    {
        // PARTICIPANT CODE HERE:

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not loop/restart
        {
            if (setup.getCodeStillRunning() == false)
            {
                StartCoroutine("code40");
                setup.setCodeStillRunning(true);
            }
        }
    }
}
































/*
//p46eval5 code
public class code5class : ExpLibrary
{

    // code info
    public static void setCode5Info()
    {
        setup.codename = "p46eval5";
        setup.code5Active = true;
        setup.eval = 5;
        setup.redblink = false;
        setup.greenblink = true;
        setup.blueblink = false;
        setup.comments = "";
    }

    // participant code below:
    int ultrasound_LP()
    {
        int u1 = ultrasound();
        int u2 = ultrasound();
        int u3 = ultrasound();
        int u4 = ultrasound();
        int u5 = ultrasound();
        return (u1 + u2 + u3 + u4 + u5) / 5;
    }

    public IEnumerator code5()
    {
        motor(30, -30);
        while (ultrasound_LP() > 100)
        {
            //
            yield return new WaitForFixedUpdate();
        }
        motor(0, 0);
        //turn(60, 5);
        yield return StartCoroutine(turnCoroutine(60, 5));
        int turn_angle = 20 - ultrasound_LP() / 12;
        //turn(60, turn_angle);
        yield return StartCoroutine(turnCoroutine(60, turn_angle));


        motor(100, 100);
        while (reflectionDown() > 42)
        {
            // do nothing
            yield return new WaitForFixedUpdate();
        }
        motor(0, 0);


        if (ultrasound_LP() < 30)
        {

            if (reflectionRedLeft() > 10 || reflectionRedRight() > 10)
            {
                PlayTone(400, 2000);
            }

            else
            {
                //wait(2000);
                yield return StartCoroutine(waitCoroutine(2000));
                if (blink > 40)
                {
                    PlayTone(800, 2000);
                }
                else
                {
                    PlayTone(1600, 2000);
                }
            }

            motor(100, 100);
            //wait(500);
            yield return StartCoroutine(waitCoroutine(500));
            motor(-100, -100);
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(1000));
            motor(0, 0);
            //turn(60, 60);
            yield return StartCoroutine(turnCoroutine(60, 60));
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
                StartCoroutine(code5());
            setup.setCodeStillRunning(true);
        }
        else
        {
            Debug.Log("code5 ended \t" + (Time.time - setup.iterationStartTime));
            setup.writeIterationSummary();
            setup.iterationCounter++;
            setup.setCodeStillRunning(false);
        }
    }
}

//bare ultrasound with color
public class code4class : code5class
{
    // code info
    public static void setCode4Info()
    {
        setup.codename = "bareUltra";
        setup.code4Active = true;
        setup.eval = 99;
        setup.redblink = false;
        setup.greenblink = true;
        setup.blueblink = false;
        setup.comments = "bare ultrasound code with color detection";
    }

    public IEnumerator code4()
    {
        if (ultrasound() < 100)
        {
            motor(100, 100);
        }
        else
            motor(100, -100);

        if (ultrasound() < 15)
        {
            if (reflectionRedLeft() > 6 || reflectionRedRight() > 6)
                PlayTone(400, 100);
            else if (blink > 15)
                PlayTone(800, 100);
            else
                PlayTone(1600, 100);
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
                StartCoroutine(code4());
            setup.setCodeStillRunning(true);
        }
        else
        {
            Debug.Log("code4 ended \t" + (Time.time - setup.iterationStartTime));
            setup.writeIterationSummary();
            setup.iterationCounter++;
            setup.setCodeStillRunning(false);
        }
    }
}

// blink solution code
public class code3class : code4class
{
    // code info
    public static void setCode3Info()
    {
        setup.codename = "blinkSolution";
        setup.code3Active = true;
        setup.eval = 99;
        setup.redblink = true;
        setup.greenblink = true;
        setup.blueblink = true;
        setup.comments = "code for calibrating with blink solution with starting configurations of eval 1 to 3";
    }

    //blinkcode
    public IEnumerator code3()
    {

        for (int i = 0; i < 18; i++)
        {
            //dispNum(0, 0, blink);
            //Debug.Log("for: " + blink);
            //turn(60, 20);
            yield return StartCoroutine(turnCoroutine(60, 20));
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(1000));
            if (blink >= 14)
            {
                //Debug.Log("break engaged");
                //PlayTone(440, 100);
                //turn(60, 20);
                yield return StartCoroutine(turnCoroutine(60, 20));
                //wait(1000);
                yield return StartCoroutine(waitCoroutine(1000));
                yield return new WaitForFixedUpdate();
                //Debug.Log("for2: " + blink);
                break;
            }
        }

        startTimer1();

        while (reflectionDown() > 42 && readTimer1() < 1000)
        {

            if (blink < 7 && readTimer1() < 1000)
            {
                //Debug.Log("white search " + blink);
                //Debug.Log("timer " + readTimer1());
                //Debug.Log("blink < 7");
                //turn(60, 20);
                yield return StartCoroutine(turnCoroutine(60, 20));
                //wait(800);
                yield return StartCoroutine(waitCoroutine(800));
            }
            else
            {
                //Debug.Log("push" + blink);
                motor(100, 100);
                //PlayTone(880, 100);
                startTimer1();
            }
            yield return new WaitForFixedUpdate();
        }

        //Debug.Log("timer ran out");

        startTimer2();
        while (reflectionDown() > 42 && readTimer2() < 2000)
        {
            //Debug.Log("driving free");
            motor(100, 100);
            yield return new WaitForFixedUpdate();
        }


        //wait(300);
        yield return StartCoroutine(waitCoroutine(300));
        motor(0, 0);
        //wait(300);
        yield return StartCoroutine(waitCoroutine(300));

        if (reflectionDown() < 42)
        {
            //Debug.Log(reflectionDown());
            //PlayTone(1200, 100);
            motor(-100, -100);
            //wait(300);
            yield return StartCoroutine(waitCoroutine(300));
            while (reflectionDown() < 42)
            {
                motor(100, -100);
                yield return new WaitForFixedUpdate();
            }
            //turn(60, 90);
            yield return StartCoroutine(turnCoroutine(60, 90));
            motor(100, 100);
            //wait(1000);
            yield return StartCoroutine(waitCoroutine(300));
            motor(0, 0);
        }

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
                StartCoroutine(code3());
            setup.setCodeStillRunning(true);
        }
        else
        {
            Debug.Log("code3 ended \t" + (Time.time - setup.iterationStartTime));
            setup.writeIterationSummary();
            setup.iterationCounter++;
            setup.setCodeStillRunning(false);
        }
    }
}

//minimalstructuredcode
public class code2class : code3class
{
    // code info
    public static void setCode2Info()
    {
        setup.codename = "minstruct";
        setup.code2Active = true;
        setup.eval = 99;
        setup.redblink = false;
        setup.greenblink = false;
        setup.blueblink = false;
        setup.comments = "";
    }

    bool onCardboard()
    {
        if (reflectionDown() <= 42)
            return true;
        else
            return false;
    }

    bool cubeFound()
    {
        if (ultrasound() < 120)
            return true;
        else
            return false;
    }


    public IEnumerator code2()
    {

        if (onCardboard())
        {
            while (onCardboard())
            {
                motor(-60, -60);
                yield return new WaitForFixedUpdate();
            }
            //turn(100, 135);
            yield return StartCoroutine(turnCoroutine(100, 135));
        }

        else
        {
            if (cubeFound())
            {
                motor(100, 100);
                //wait(200);
                yield return StartCoroutine(waitCoroutine(200));
            }
            else
                motor(100, -100);
        }


        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
                StartCoroutine(code2());
            setup.setCodeStillRunning(true);
        }
        else
        {
            Debug.Log("code2 ended \t" + (Time.time - setup.iterationStartTime));
            setup.writeIterationSummary();
            setup.iterationCounter++;
            setup.setCodeStillRunning(false);
        }
    }
}

// random110
public class code1class : code2class
{
    // code info
    public static void setCode1Info()
    {
        setup.codename = "random110";
        setup.code1Active = true;
        setup.eval = 99;
        setup.redblink = false;
        setup.greenblink = false;
        setup.blueblink = false;
        setup.comments = "";
    }

    

    public IEnumerator code1()
    {

        if (reflectionDown() > 42)
            motor(100, 100);
        else
        {
            yield return StartCoroutine(turnCoroutine(100, 110));
        }

       

        // endless while(true) loop from the participant until task is completed or failed
        yield return new WaitForFixedUpdate();
        setup.setCodeStillRunning(false);
        if (!((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime))     // if all cubes are outside or the robot fell off the coroutine shall not restart
        {
            if (setup.getCodeStillRunning() == false)
                StartCoroutine(code1());
            setup.setCodeStillRunning(true);
        }
        else
        {
            Debug.Log("code1 ended \t" + (Time.time - setup.iterationStartTime));
            setup.writeIterationSummary();
            setup.iterationCounter++;
            setup.setCodeStillRunning(false);
        }
    }
}
*/
