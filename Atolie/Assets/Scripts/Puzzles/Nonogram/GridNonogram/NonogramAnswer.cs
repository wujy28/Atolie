using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramAnswer : MonoBehaviour
{
    //Contains the answers for all three stages

    public bool[] firstStageAnswer = {false, false, true, true,  false,
                                      false, false, true, false, true,
                                      false, false, true, false, true,
                                      true,  true,  true, false, false,
                                      true,  true,  true, false, false};

    public bool[] secondStageAnswer = {false, false, true,  true,  true,  false, false,
                                       false, true,  false, false, false, true,  false,
                                       true,  false, true,  true,  true,  false, true,
                                       true,  false, false, false, false, false, true,
                                       false, true,  false, false, false, true,  false,
                                       false, false, true,  true,  true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  true,  true,  false, false,
                                       false, false, false, true,  false, false, false,
                                       false, false, true,  false, false, false, false,
                                       false, false, true,  false, false, false, false,
                                       false, false, false, true,  true,  false, false};

    public bool[] thirdStageAnswer = {false, false, false, false, false, false, false, false, false, false, false, false, true,  false,
                                      false, false, false, false, false, false, false, false, false, false, false, true,  false, true,
                                      false, false, false, false, false, false, false, false, false, false, true,  false, false, true,
                                      false, false, false, false, false, false, false, false, false, false, true,  false, false, true,
                                      false, false, false, false, false, false, true,  false, false, true,  false, true,  true,  false,
                                      false, false, false, false, false, true,  true,  false, true,  false, true,  false, false, false,
                                      false, false, false, false, false, true,  false, true,  false, true,  false, false, false, false,
                                      false, false, true,  true,  true,  false, true,  false, true,  false, false, false, false, false,
                                      false, true,  false, false, false, true,  false, true,  false, true,  true,  false, false, false,
                                      true,  false, false, false, true,  false, true,  false, true,  true,  false, false, false, false,
                                      true,  false, false, false, true,  true,  false, true,  false, false, false, false, false, false,
                                      true,  false, false, false, false, false, false, true,  false, false, false, false, false, false,
                                      false, true,  false, false, false, false, false, true,  false, false, false, false, false, false,
                                      false, false, true,  false, false, false, true,  false, false, false, false, false, false, false,
                                      false, false, false, true,  true,  true,  false, false, false, false, false, false, false, false};
}
